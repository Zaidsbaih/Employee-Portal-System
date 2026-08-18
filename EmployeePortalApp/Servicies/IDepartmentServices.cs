using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public interface IDepartmentServices
    {

        void insert(DepartmentsModel model);
        List<DepartmentsModel> LoadAll();
        void Delete(int id);
        DepartmentsModel Load(int id);
        void Update(DepartmentsModel model);

    }
}
