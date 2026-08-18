using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortalApp.data
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;

        [Required]
        public DateTime ClockIn { get; set; }

        public DateTime? ClockOut { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
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
