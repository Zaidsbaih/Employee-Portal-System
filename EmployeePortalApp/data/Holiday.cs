using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.data
{
    public class Holiday
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
