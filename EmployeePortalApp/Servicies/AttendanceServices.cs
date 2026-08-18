using AutoMapper;
using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalApp.Servicies
{
    public class AttendanceServices : IAttendanceServices
    {
        EmployeeContext context;
        IMapper mapper;
        public AttendanceServices(EmployeeContext _context, IMapper _mapper)
        {
            context = _context;
            mapper = _mapper;
        }
        public string ClockIn(int employeeId)
        {
            DateTime now = DateTime.Now;

            TimeSpan startTime = new TimeSpan(9, 0, 0);
            TimeSpan endTime = new TimeSpan(17, 0, 0);

            if (now.TimeOfDay < startTime)
                return "Clock In starts at 9:00 AM.";

            if (now.TimeOfDay >= endTime)
                return "You cannot clock in at or after 5:00 PM.";

            bool alreadyClockedIn = context.Attendances.Any(a =>
                a.EmployeeId == employeeId &&
                a.ClockIn.Date == now.Date);

            if (alreadyClockedIn)
                return "You have already clocked in today.";

            Attendance attendance = new Attendance
            {
                EmployeeId = employeeId,
                ClockIn = now
            };

            context.Attendances.Add(attendance);
            context.SaveChanges();

            return "Success";
        }
        public string ClockOut(int employeeId)
        {
            DateTime now = DateTime.Now;

            Attendance? attendance = context.Attendances
                .Where(a =>
                    a.EmployeeId == employeeId &&
                    a.ClockIn.Date == now.Date &&
                    a.ClockOut == null)
                .OrderByDescending(a => a.ClockIn)
                .FirstOrDefault();

            if (attendance == null)
                return "You must Clock In first.";

            if (now.TimeOfDay < new TimeSpan(17, 0, 0))
                return "Clock Out is allowed at 5:00 PM.";

            attendance.ClockOut = now;

            context.SaveChanges();

            return "Success";
        }
        public void AutoClockOut(int employeeId)
        {
            DateTime today = DateTime.Today;

            List<Attendance> openAttendances = context.Attendances
                .Where(a =>
                    a.EmployeeId == employeeId &&
                    a.ClockOut == null &&
                    a.ClockIn.Date < today)
                .ToList();

            if (openAttendances.Count == 0)
                return;

            foreach (Attendance attendance in openAttendances)
            {
                attendance.ClockOut =
                    attendance.ClockIn.Date.AddHours(17);
            }

            context.SaveChanges();
        }

        public AttendanceModel? GetTodayAttendance(int employeeId)
        {
            AutoClockOut(employeeId);

            DateTime today = DateTime.Today;

            Attendance? attendance = context.Attendances
                .Where(a =>
                    a.EmployeeId == employeeId &&
                    a.ClockIn.Date == today)
                .OrderByDescending(a => a.ClockIn)
                .FirstOrDefault();

            if (attendance == null)
                return null;

            return new AttendanceModel
            {
                Id = attendance.Id,
                EmployeeId = attendance.EmployeeId,
                ClockIn = attendance.ClockIn,
                ClockOut = attendance.ClockOut
            };
        }
        public AttendanceHistoryVM GetAttendanceHistory(int employeeId, DateTime? fromDate, DateTime? toDate) 
        {
            AutoClockOut(employeeId);

            DateTime today = DateTime.Today;

            IQueryable<Attendance> query = context.Attendances
                .Where(a => a.EmployeeId == employeeId);

            if (fromDate.HasValue)
            {
                query = query.Where(a =>
                    a.ClockIn.Date >= fromDate.Value.Date);
            }

            if (toDate.HasValue)
            {
                query = query.Where(a =>
                    a.ClockIn.Date <= toDate.Value.Date);
            }

            List<AttendanceModel> attendanceList = query
                .OrderByDescending(a => a.ClockIn)
                .Select(a => new AttendanceModel
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    ClockIn = a.ClockIn,
                    ClockOut = a.ClockOut
                })
                .ToList();

            AttendanceModel? todayAttendance = context.Attendances
                .Where(a =>
                    a.EmployeeId == employeeId &&
                    a.ClockIn.Date == today)
                .OrderByDescending(a => a.ClockIn)
                .Select(a => new AttendanceModel
                {
                    Id = a.Id,
                    EmployeeId = a.EmployeeId,
                    ClockIn = a.ClockIn,
                    ClockOut = a.ClockOut
                })
                .FirstOrDefault();

            AttendanceHistoryVM model = new AttendanceHistoryVM
            {
                TodayAttendance = todayAttendance,
                AttendanceList = attendanceList,
                FromDate = fromDate,
                ToDate = toDate
            };

            return model;
        }
    }
}
