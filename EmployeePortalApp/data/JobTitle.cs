using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.data
{
    public class JobTitle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }
}
