using EmployeePortalApp.data;
using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public interface IEmployeeServicies
    {
        Task insert(EmployeeModel Model);
        List<EmployeeModel> LoadAll();
        List<Departments> GetDepartments();
        List<JobTitle> GetJobTitles();
        List<Employee> GetManagers();
        void Delete(int id);
        List<EmployeeModel> SearchEmployee(String name, String Phone);
        void SetPassword(SetPasswordModel model);
        EmployeeModel Load(int id);
        void Update(EmployeeModel model);

        Task<bool> ForgotPassword(ForgotPasswordModel model);
        EmployeeModel? Login(Login_EmpModel model);
        DashboardModel? GetDashboard(int employeeId);




    }
}
