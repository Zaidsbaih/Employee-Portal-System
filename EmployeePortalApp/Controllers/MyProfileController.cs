using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using EmployeePortalApp.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalApp.Controllers
{
    public class MyProfileController : Controller
    {
        IMyProfileServices myProfileServices;
        public MyProfileController(IMyProfileServices _myProfileServices) 
        {
            myProfileServices= _myProfileServices;
        }
        public IActionResult MyProfile()
        {
            int? employeeId = HttpContext.Session.GetInt32("EmployeeId");
            if (employeeId == null)
                return RedirectToAction("Login_emp", "Employee");
            EmployeeModel model = myProfileServices.GetMyProfile(employeeId.Value);
            return View(model);
        }
    }
}
