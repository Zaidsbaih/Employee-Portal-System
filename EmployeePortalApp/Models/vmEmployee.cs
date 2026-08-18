using EmployeePortalApp.servicies;

namespace EmployeePortalApp.Models
{
    public class vmEmployee
    {
        public EmployeeModel employeeModel { get; set; }
        public List<DepartmentsModel> departmentsList { get; set; }
        public List<JobTitleModel> jobTitleList { get; set; }
        public List<EmployeeModel> ManagersList { get; set; }

    }
}
