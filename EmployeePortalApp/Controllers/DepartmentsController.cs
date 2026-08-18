using EmployeePortalApp.Models;
using EmployeePortalApp.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalApp.Controllers
{
    public class DepartmentsController : Controller
    {
        IDepartmentServices departmentServices;
        public DepartmentsController(IDepartmentServices _departmentServices) 
        {
            departmentServices = _departmentServices;
        }
        public IActionResult NewDepartments()
        {
            ViewBag.IsEdit = false;
            return View();
        }
        public IActionResult SaveDepartment( DepartmentsModel model) 
        {
            departmentServices.insert(model);
            return RedirectToAction("DepartmentsList");
        }
        public IActionResult DepartmentsList()
        {
            List<DepartmentsModel> departmentsModels = departmentServices.LoadAll();
            return View(departmentsModels);
        }
        public IActionResult DeleteDepartment(int id)
        {
            departmentServices.Delete(id);
            return RedirectToAction("DepartmentsList");
        }
        public IActionResult EditDepartment (int id)
        {
                ViewBag.IsEdit = true;
            DepartmentsModel departmentsModel = departmentServices.Load(id);
            return View("NewDepartments", departmentsModel);
        }
        public IActionResult UpdateDepartment(DepartmentsModel model)
        {
            ViewBag.IsEdit = true;
            departmentServices.Update(model);
            return RedirectToAction("DepartmentsList");
        }
    }
}
