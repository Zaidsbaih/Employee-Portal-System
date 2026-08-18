using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public interface IPolicyServices
    {
        void Insert(PolicyModel model);

        List<PolicyModel> LoadAll();

        PolicyModel? Load(int id);

        void Update(PolicyModel model);

        void Delete(int id);

        List<PolicyModel> Search(string title);
    }
}