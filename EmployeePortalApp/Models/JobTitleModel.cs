using AutoMapper;
using EmployeePortalApp.data;

namespace EmployeePortalApp.Models
{
    [AutoMap(typeof(JobTitle),ReverseMap = true)]
    public class JobTitleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
