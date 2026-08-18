using AutoMapper;
using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace EmployeePortalApp.Servicies
{
    public class JobTitleServices : IJobTitleServices
    {
        IMapper mapper;
        EmployeeContext context;
        public JobTitleServices(IMapper _mapper, EmployeeContext _context)
        {
            mapper = _mapper;
            context = _context;
        }

        public void insert (JobTitleModel Model1)
        {
            JobTitle j  = mapper.Map<JobTitle>(Model1);
            context.JobTitles.Add(j);
            context.SaveChanges();
        }

        public List<JobTitleModel> loadAll()
        {
            List<JobTitle> AllTitle =context.JobTitles.ToList();
            List<JobTitleModel>AllTitleModel = new List<JobTitleModel>();
            AllTitleModel = mapper.Map<List<JobTitleModel>>(AllTitle);
            return AllTitleModel;
        }
        public void Delete(int id) 
        {
            JobTitle j = context.JobTitles.Find(id);
            if (j != null)
            {
                context.JobTitles.Remove(j);
                context.SaveChanges();
            }
        }
        public JobTitleModel load(int id) 
        {
            JobTitle j = context.JobTitles.Find(id);
            if (j != null)
            {
                JobTitleModel jobTitleModel = mapper.Map<JobTitleModel>(j);
                return jobTitleModel;
            }
            else
                return null;
        }
        public void update(JobTitleModel model) 
        {
            JobTitle job = context.JobTitles.Find(model.Id);
            if(job != null)
            {
                job.Title = model.Title;
                context.SaveChanges();
            }
            
        }
    }
}
