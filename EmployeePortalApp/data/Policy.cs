using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.data
{
    public class Policy
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string Category { get; set; }

        public string? Description { get; set; }

        [Required]
        public string FilePath { get; set; }

        public DateTime UploadDate { get; set; }

        public bool IsActive { get; set; }
    }
}
