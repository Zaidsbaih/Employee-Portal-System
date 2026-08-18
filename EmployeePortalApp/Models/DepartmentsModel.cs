using AutoMapper;
using EmployeePortalApp.data;
using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.Models
{
    [AutoMap(typeof(Departments), ReverseMap =true)]
    public class DepartmentsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
