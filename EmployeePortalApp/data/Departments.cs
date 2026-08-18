namespace EmployeePortalApp.data
{
    using System.ComponentModel.DataAnnotations;


    public class Departments
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public ICollection<Employee>? Employees { get; set; }
    }

}