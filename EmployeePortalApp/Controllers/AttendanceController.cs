using EmployeePortalApp.Models;
using EmployeePortalApp.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalApp.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceServices attendanceServices;

        public AttendanceController(
            IAttendanceServices _attendanceServices)
        {
            attendanceServices = _attendanceServices;
        }

        [HttpPost]
        public IActionResult ClockIn()
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (employeeId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }

            string result =
                attendanceServices.ClockIn(employeeId.Value);

            if (result == "Success")
            {
                TempData["AttendanceSuccess"] =
                    "Clock In completed successfully.";
            }
            else
            {
                TempData["AttendanceError"] = result;
            }

            return RedirectToAction(
                "Dashboard",
                "Employee");
        }

        [HttpPost]
        public IActionResult ClockOut()
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (employeeId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }

            string result =
                attendanceServices.ClockOut(employeeId.Value);

            if (result == "Success")
            {
                TempData["AttendanceSuccess"] =
                    "Clock Out completed successfully.";
            }
            else
            {
                TempData["AttendanceError"] = result;
            }

            return RedirectToAction(
                "Dashboard",
                "Employee");
        }
        [HttpGet]
        public IActionResult AttendanceHistory(DateTime? fromDate,DateTime? toDate)
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (employeeId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }
            AttendanceHistoryVM model = attendanceServices.GetAttendanceHistory(employeeId.Value, fromDate,
            toDate);
            return View(model);
        }
    }
}