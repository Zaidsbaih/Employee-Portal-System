using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public interface IJobTitleServices
    {
        void insert(JobTitleModel jobTitleModel);
        public List<JobTitleModel> loadAll();
        void Delete(int id);
        JobTitleModel load(int id);
        void update(JobTitleModel model);





    }
}
