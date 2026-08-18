using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortalApp.data
{
    public class LeaveRequest
    {
        [Key]
        public int Id { get; set; }

        // الموظف الذي قدم طلب الإجازة
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;


        // المدير الذي سيوافق أو يرفض الطلب
        public int? ApprovalManagerId { get; set; }

        [ForeignKey(nameof(ApprovalManagerId))]
        public Employee? ApprovalManager { get; set; }


        [Required]
        [StringLength(30)]
        public string LeaveType { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }


        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }


        [Required]
        [StringLength(500)]
        public string Reason { get; set; } = string.Empty;


        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Pending";


        [StringLength(500)]
        public string? ManagerComment { get; set; }


        public DateTime RequestDate { get; set; } = DateTime.Now;


        public DateTime? DecisionDate { get; set; }
    }
}