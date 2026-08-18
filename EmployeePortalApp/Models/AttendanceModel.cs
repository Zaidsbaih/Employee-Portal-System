namespace EmployeePortalApp.Models
{
    public class AttendanceModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime ClockIn { get; set; }

        public DateTime? ClockOut { get; set; }
        public DateTime Date { get; set; }

        public TimeSpan? WorkingHours
        {
            get
            {
                if (ClockOut == null)
                    return null;

                return ClockOut.Value - ClockIn;
            }
        }
    }
}
