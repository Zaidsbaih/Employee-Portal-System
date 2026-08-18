namespace EmployeePortalApp.Models
{
    public class AttendanceHistoryVM
    {
        // حضور اليوم
        public AttendanceModel? TodayAttendance { get; set; }

        // جدول السجل
        public List<AttendanceModel> AttendanceList { get; set; }
            = new List<AttendanceModel>();

        // الفلترة
        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}