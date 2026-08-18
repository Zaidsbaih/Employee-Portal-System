using AutoMapper;
using EmployeePortalApp.data;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;

namespace EmployeePortalApp.Models
{
    [AutoMap(typeof(Employee),ReverseMap = true )]
    public class EmployeeModel
    {
        public string FullName { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime JoiningDate { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }
        public int JobTitleId { get; set; }
        public int? ManagerId { get; set; }
        public IFormFile? Photo { get; set; }
        public string? PhotoPath { get; set; }
        public string DepartmentName { get; set; }

        public string JobTitleName { get; set; }
        public DateTime? BirthDate { get; set; }

        public string? ManagerName { get; set; }
        public int Id { get; set; }
    }
}