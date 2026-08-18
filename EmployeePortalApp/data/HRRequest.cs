using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeePortalApp.data
{
    public class HRRequest
    {
        [Key]
        public int Id { get; set; }

        // الموظف الذي قدم الطلب
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; } = null!;


        // نوع الطلب
        [Required]
        [StringLength(100)]
        public string RequestType { get; set; } = string.Empty;


        // تفاصيل يكتبها الموظف
        [StringLength(500)]
        public string? Description { get; set; }


        // New / In Progress / Completed
        [Required]
        [StringLength(30)]
        public string Status { get; set; } = "New";


        // تعليق HR
        [StringLength(500)]
        public string? HRComment { get; set; }


        // الملف الذي يرفعه HR
        public string? AttachmentPath { get; set; }


        public DateTime RequestDate { get; set; } = DateTime.Now;


        public DateTime? CompletedDate { get; set; }
    }
}