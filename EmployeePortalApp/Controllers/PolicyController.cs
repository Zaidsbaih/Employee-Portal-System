using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using EmployeePortalApp.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalApp.Controllers
{
    public class PolicyController : Controller
    {
        EmployeeContext context;
        IPolicyServices Policyservices;
        public PolicyController (EmployeeContext _context , IPolicyServices _Policyservices)
        {
            context = _context;
            Policyservices = _Policyservices;
        }
        public IActionResult Policy()
        {
            return View();
        }
        public IActionResult NewPolicy()
        {
            ViewBag.IsEdit = false;

            return View();
        }
        [HttpPost]
        public IActionResult SavePolicy(PolicyModel model)
        {
            Policyservices.Insert(model);

            return RedirectToAction("PolicyList");
        }
        public IActionResult PolicyList()
        {
            List<PolicyModel> policies =
                Policyservices.LoadAll();

            return View(policies);
        }
        public IActionResult EditPolicy(int id)
        {
            ViewBag.IsEdit = true;

            PolicyModel? model =
                Policyservices.Load(id);

            if (model == null)
                return NotFound();

            return View("NewPolicy", model);
        }

        [HttpPost]
        public IActionResult UpdatePolicy(PolicyModel model)
        {
            Policyservices.Update(model);

            return RedirectToAction("PolicyList");
        }
        public IActionResult DeletePolicy(int id)
        {
            Policyservices.Delete(id);

            return RedirectToAction("PolicyList");
        }
        public IActionResult Policies(string title)
        {
            List<PolicyModel> policies =
                Policyservices.Search(title);

            return View(policies);
        }
    }
}
