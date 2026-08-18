using EmployeePortalApp.Models;
using EmployeePortalApp.Servicies;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortalApp.Controllers
{
    public class LeaveController : Controller
    {
        ILeaveServices leaveServices;
        public LeaveController(ILeaveServices _leaveServices)
        {
            leaveServices = _leaveServices;
        }
        public IActionResult LeaveRequest()
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");
            if (employeeId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }


            LeaveRequestVM model = leaveServices.GetLeaveRequestPage(employeeId.Value);
            return View(model);
        }

        [HttpPost]
        public IActionResult SubmitLeave(LeaveRequestVM vm)
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");
            if (employeeId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }
            leaveServices.SubmitLeave(employeeId.Value, vm.LeaveRequest);
            return RedirectToAction("LeaveRequest");
        }
        public IActionResult EditLeave(int id)
        {
            int? employeeId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (employeeId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }

            LeaveRequestModel model =
                leaveServices.Load(id);

            if (model == null)
                return NotFound();

            LeaveRequestVM vm =
                leaveServices.GetLeaveRequestPage(employeeId.Value);

            vm.LeaveRequest = model;

            ViewBag.IsEdit = true;

            return View("LeaveRequest", vm);
        }



        [HttpPost]
        public IActionResult UpdateLeave(LeaveRequestVM vm)
        {
            leaveServices.Update(vm.LeaveRequest);

            return RedirectToAction("LeaveRequest");
        }
        [HttpPost]
        public IActionResult CancelLeave(int id)
        {
            leaveServices.Cancel(id);

            return RedirectToAction("LeaveRequest");
        }

        public IActionResult ApprovalRequests(string? employeeName,string? status,DateTime? fromDate,DateTime? toDate)
        {
            int? managerId = HttpContext.Session.GetInt32("EmployeeId");
            if (managerId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }
            bool isManager =leaveServices.IsManager(managerId.Value);
            if (!isManager)
            {
                return RedirectToAction(
                    "Dashboard",
                    "Employee");
            }
            ApprovalRequestsVM vm = leaveServices.GetApprovalRequests
                (
            managerId.Value,
            employeeName,
            status,
            fromDate,
            toDate
            );
            return View(vm);
        }
        [HttpPost]
        public IActionResult Approve( int leaveId,string? comment)
        {
            int? managerId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (managerId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }

            leaveServices.Approve(
                leaveId,
                managerId.Value,
                comment);

            return RedirectToAction("ApprovalRequests");
        }
        [HttpPost]
        public IActionResult Reject(int leaveId,string? comment)
        {
            int? managerId =
                HttpContext.Session.GetInt32("EmployeeId");

            if (managerId == null)
            {
                return RedirectToAction(
                    "Login_emp",
                    "Employee");
            }

            leaveServices.Reject(leaveId,managerId.Value,comment);

            return RedirectToAction("ApprovalRequests");
        }
    }
}
