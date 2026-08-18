namespace EmployeePortalApp.Models
{
    public class DashboardModel
    {
        // الموظف المسجل دخوله
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        public string? PhotoPath { get; set; }

        public string? DepartmentName { get; set; }

        public string? JobTitleName { get; set; }


        // حضور الموظف اليوم
        public AttendanceModel? TodayAttendance { get; set; }


        // العطل القادمة
        public List<HolidayModel> UpcomingHolidays { get; set; }
            = new List<HolidayModel>();


        // أعياد الميلاد اليوم
        public List<EmployeeModel> TodayBirthdays { get; set; }
            = new List<EmployeeModel>();
    }
}