using EmployeePortalApp.Models;
using EmployeePortalApp.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalApp.Controllers
{
    [Route("HRRequest")]
    public class HRRequestController : Controller
    {
        IHRRequestServices hrRequestServices;

        public HRRequestController(
            IHRRequestServices _hrRequestServices)
        {
            hrRequestServices = _hrRequestServices;
        }


        // ==============================
        // Employee
        // ==============================

        [HttpGet("EmployeeRequests")]
        public IActionResult EmployeeRequests()
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (employeeId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }

            HRRequestEmployeeVM vm =
                hrRequestServices.GetEmployeeRequests(
                    employeeId.Value);

            return View("EmployeeRequests", vm);
        }


        [HttpPost("SubmitRequest")]
        public IActionResult SubmitRequest(
            HRRequestEmployeeVM vm)
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (employeeId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }

            hrRequestServices.SubmitRequest(
                employeeId.Value,
                vm.NewRequest);

            return RedirectToAction(
                nameof(EmployeeRequests));
        }


        // ==============================
        // Admin / HR
        // ==============================

        [HttpGet("AdminRequests")]
        public IActionResult AdminRequests(
            string? employeeName,
            string? requestType,
            string? status)
        {
            HRRequestAdminVM vm =
                hrRequestServices.GetAdminRequests(
                    employeeName,
                    requestType,
                    status);

            return View("AdminRequests", vm);
        }


        [HttpPost("UpdateRequest")]
        public IActionResult UpdateRequest(
            int requestId,
            string status,
            string? comment,
            IFormFile? attachmentFile)
        {
            hrRequestServices.UpdateRequest(
                requestId,
                status,
                comment,
                attachmentFile);

            return RedirectToAction(
                nameof(AdminRequests));
        }
    }
}