

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using EmployeePortalApp.servicies;
using EmployeePortalApp.Servicies;

namespace EmployeePortalApp.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeServicies employeeServicies;
        IEmailService emailService;
        IDepartmentServices departmentServices;
        IJobTitleServices jobTitleServices;
        IAttendanceServices attendanceServices;
        ILeaveServices leaveServices;


        public EmployeeController(
            IEmployeeServicies _employeeServicies,
            IDepartmentServices _departmentServices,
            IJobTitleServices _jobTitleServices,
            IEmailService _emailService,
            IAttendanceServices _attendanceServices,
            ILeaveServices _leaveServices)
        {
            employeeServicies = _employeeServicies;
            departmentServices = _departmentServices;
            jobTitleServices = _jobTitleServices;
            emailService = _emailService;
            attendanceServices = _attendanceServices;
            leaveServices = _leaveServices;
        }
        

        

        public IActionResult Login_emp()
        {
            return View();
        }
        public IActionResult NewEmployee()
        {
            ViewBag.IsEdit = false;
            vmEmployee vm = new vmEmployee();
            vm.employeeModel = new EmployeeModel();
            vm.departmentsList = departmentServices.LoadAll() ;
            vm.jobTitleList = jobTitleServices.loadAll();
            vm.ManagersList = employeeServicies.LoadAll();

            return View(vm);
            
        }


        [HttpPost]
        public async Task<IActionResult> SaveEmployee(vmEmployee vm)
        {
            await employeeServicies.insert(vm.employeeModel);

            return RedirectToAction("NewEmployee");
        }
        public IActionResult EmployeeList()
        {
            List<EmployeeModel>employees = employeeServicies.LoadAll();
            return View(employees);
        }
       
        
        public IActionResult SetPassword(string token)
        {
            var model = new SetPasswordModel
            {
                Token = token
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult SetPassword(SetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            employeeServicies.SetPassword(model);

            return RedirectToAction("Login_emp");
        }
        
        
        public IActionResult SearchEmployee()
        {
            String name = Request.Form["textEmployeeName"];
            String Phone = Request.Form["textEmployeePhone"];
            List<EmployeeModel> employees = employeeServicies.SearchEmployee(name, Phone);


            return View("EmployeeList",employees);
        }
        public IActionResult DeleteEmployee(int id)
        {
            employeeServicies.Delete(id);
            return RedirectToAction("EmployeeList");
        }
        public IActionResult EditEmployee(int id)
        {
            ViewBag.IsEdit = true;
            vmEmployee vm = new vmEmployee();
            vm.employeeModel = employeeServicies.Load(id);
            vm.departmentsList = departmentServices.LoadAll();
            vm.jobTitleList = jobTitleServices.loadAll();
            vm.ManagersList = employeeServicies.LoadAll();
            return View("NewEmployee",vm);
        }
        public IActionResult UpdateEmployee(vmEmployee model) 
        {
            ViewBag.IsEdit = true;
            employeeServicies.Update(model.employeeModel);
            return RedirectToAction("EmployeeList");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            bool result = await employeeServicies.ForgotPassword(model);

            if (!result)
            {
                ModelState.AddModelError("", "Email not found.");
                return View(model);
            }

            TempData["Message"] = "A reset password link has been sent to your email.";

            return RedirectToAction("Login_emp");
        }
        [HttpPost]
        public IActionResult Login(Login_EmpModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Login_emp", model);
            }

            EmployeeModel? employeeModel =
                employeeServicies.Login(model);

            if (employeeModel == null)
            {
                ModelState.AddModelError(
                    "",
                    "Invalid email or password.");

                return View("Login_emp", model);
            }

            HttpContext.Session.SetInt32(
    "EmployeeId",
    employeeModel.Id);

            HttpContext.Session.SetString(
                "EmployeeName",
                employeeModel.FullName);

            bool isManager =
                leaveServices.IsManager(employeeModel.Id);

            
            HttpContext.Session.SetString(
                "IsManager",
                isManager.ToString());

            return RedirectToAction("Dashboard");
        }
        public IActionResult Dashboard()
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (employeeId == null)
            {
                return RedirectToAction("Login_emp");
            }
            attendanceServices.AutoClockOut(employeeId.Value);
            DashboardModel? model =
                employeeServicies.GetDashboard(employeeId.Value);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login_emp");
        }

    }
}

