using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.Models
{
    public class LeaveRequestModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public int? ApprovalManagerId { get; set; }

        [Required]
        public string LeaveType { get; set; } = "";

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public string Reason { get; set; } = "";

        public string Status { get; set; } = "";

        public string? ManagerComment { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? DecisionDate { get; set; }

        // للعرض فقط
        public string? EmployeeName { get; set; }

        public string? ManagerName { get; set; }
    }
}