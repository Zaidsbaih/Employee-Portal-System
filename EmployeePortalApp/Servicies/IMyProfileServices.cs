using EmployeePortalApp.data;
using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public interface IMyProfileServices
    {
        EmployeeModel? GetMyProfile(int employeeId);
    }
}
