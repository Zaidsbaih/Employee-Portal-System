using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.data
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;


    [Index(nameof(Email), IsUnique = true)]
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }

        public string? Password { get; set; }

        public string? PasswordSetupToken { get; set; }

        public DateTime? PasswordSetupTokenExpiry { get; set; }

        [Required]
        [Phone]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required]
        public DateTime JoiningDate { get; set; }

        [StringLength(255)]
        public string? PhotoPath { get; set; }
        public bool IsActive { get; set; } = true;

        public DateTime? BirthDate { get; set; }

        // Department
        [Required]
        public int DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Departments Department { get; set; }

        // Job Title
        [Required]
        public int JobTitleId { get; set; }

        [ForeignKey(nameof(JobTitleId))]
        public JobTitle JobTitle { get; set; }

        // Manager
        public int? ManagerId { get; set; }

        [ForeignKey(nameof(ManagerId))]
        public Employee? Manager { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}


