using EmployeePortalApp.Models;
using EmployeePortalApp.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalApp.Controllers
{
    public class JobTitleController : Controller
    {
        IJobTitleServices jobTitleServices;
        public JobTitleController(IJobTitleServices _jobTitleServices) 
        {
            jobTitleServices= _jobTitleServices;
        }
        public IActionResult CreateJobTitle()
        {
            ViewBag.IsEdit = false;
            return View();
        }
        public IActionResult SaveJobTitle(JobTitleModel Model) 
        {
            jobTitleServices.insert(Model);
            return RedirectToAction("JobTitleList");
        }
        public IActionResult JobTitleList()
        {
            List<JobTitleModel> jobTitleModels = jobTitleServices.loadAll();
            return View(jobTitleModels);
        }
        public IActionResult DeleteJobTitle(int Id)
        {
            jobTitleServices.Delete(Id);
            return RedirectToAction("JobTitleList");
        }
        public IActionResult EditJobtitle(int Id) 
        {
            ViewBag.IsEdit = true;
            JobTitleModel jobTitleModel =jobTitleServices.load(Id);
            return View("CreateJobTitle", jobTitleModel);
        }
        public IActionResult UpdateJobTitle(JobTitleModel model)
        {
            jobTitleServices.update(model);
            ViewBag.IsEdit = true;
            return RedirectToAction("JobTitleList");
        }
    }
}
