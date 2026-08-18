using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public interface IAttendanceServices
    {
        string ClockIn(int employeeId);

        string ClockOut(int employeeId);

        void AutoClockOut(int employeeId);

        AttendanceModel? GetTodayAttendance(int employeeId);
        AttendanceHistoryVM GetAttendanceHistory(int employeeId, DateTime? fromDate, DateTime? toDate);
    }
}
