using EmployeePortalApp.data;
using EmployeePortalApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeePortalApp.Servicies
{
    public class LeaveServices : ILeaveServices
    {
        EmployeeContext Context;
        public LeaveServices(EmployeeContext _Context)
        {
            Context= _Context; 
        }
        public void SubmitLeave (int id,LeaveRequestModel model)
        {
            Employee employee = Context.Employees.Find(id);
            if (employee == null)
                return;
            LeaveRequest leaveRequest = new LeaveRequest
            {
                EmployeeId = id,
                ApprovalManagerId = employee.ManagerId,
                LeaveType = model.LeaveType,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Reason = model.Reason,
                Status = "Pending",
                RequestDate = DateTime.Now
            };
            Context.LeaveRequests.Add(leaveRequest);
            Context.SaveChanges();
        }
        public LeaveRequestVM GetLeaveRequestPage(int employeeId)
        {
            LeaveRequestVM vm = new LeaveRequestVM();
            vm.LeaveRequest=new LeaveRequestModel()
            {
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };
            vm.MyLeaves = Context.LeaveRequests.Where(i => i.EmployeeId == employeeId).OrderByDescending (i => i.Status)
                .Select(l => new LeaveRequestModel
            {
                Id = l.Id,
                EmployeeId = l.EmployeeId,
                ApprovalManagerId = l.ApprovalManagerId,
                LeaveType = l.LeaveType,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Reason = l.Reason,
                Status = l.Status,
                ManagerComment = l.ManagerComment,
                RequestDate = l.RequestDate,
                DecisionDate = l.DecisionDate
            }).ToList();
            return vm;
        }
        public LeaveRequestModel Load(int employeeId) 
        {
            LeaveRequest leave = Context.LeaveRequests.Find(employeeId);
            if (leave == null)
            { return null; }
            LeaveRequestModel model = new LeaveRequestModel
            {
                Id = leave.Id,
                EmployeeId = leave.EmployeeId,
                ApprovalManagerId = leave.ApprovalManagerId,
                LeaveType = leave.LeaveType,
                StartDate = leave.StartDate,
                EndDate = leave.EndDate,
                Reason = leave.Reason,
                Status = leave.Status,
                ManagerComment = leave.ManagerComment,
                RequestDate = leave.RequestDate,
                DecisionDate = leave.DecisionDate
            };
            return model;
        }
        public void Update(LeaveRequestModel model)
        {
            LeaveRequest leave = Context.LeaveRequests.Find(model.Id);
            if (leave != null && leave.Status == "Pending")
            {
                leave.LeaveType = model.LeaveType;
                leave.StartDate = model.StartDate;
                leave.EndDate = model.EndDate;
                leave.Reason = model.Reason;

                Context.SaveChanges();
            }
        }
        public void Cancel(int employeeId) 
        {
            LeaveRequest leave = Context.LeaveRequests.Find(employeeId);
            if(leave != null && leave.Status == "Pending")
            {
                leave.Status = "Cancelled";
                Context.SaveChanges();
            }
        }
        public bool IsManager(int employeeId)
        {
            return Context.Employees.Any(e => e.ManagerId == employeeId);
        }

        public ApprovalRequestsVM GetApprovalRequests(
    int managerId,
    string? employeeName,
    string? status,
    DateTime? fromDate,
    DateTime? toDate)
        {
            IQueryable<LeaveRequest> query = Context.LeaveRequests
                .Include(l => l.Employee)
                .Where(l => l.ApprovalManagerId == managerId);

            if (!string.IsNullOrEmpty(employeeName))
            {
                query = query.Where(l =>
                    l.Employee.FullName.Contains(employeeName));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(l =>
                    l.Status == status);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(l =>
                    l.StartDate >= fromDate.Value.Date);
            }

            if (toDate.HasValue)
            {
                query = query.Where(l =>
                    l.EndDate <= toDate.Value.Date);
            }

            ApprovalRequestsVM vm = new ApprovalRequestsVM();

            vm.Requests = query
                .OrderByDescending(l => l.RequestDate)
                .Select(l => new LeaveRequestModel
                {
                    Id = l.Id,
                    EmployeeId = l.EmployeeId,
                    EmployeeName = l.Employee.FullName,
                    LeaveType = l.LeaveType,
                    StartDate = l.StartDate,
                    EndDate = l.EndDate,
                    Reason = l.Reason,
                    Status = l.Status,
                    ManagerComment = l.ManagerComment,
                    RequestDate = l.RequestDate,
                    DecisionDate = l.DecisionDate
                })
                .ToList();

            vm.EmployeeName = employeeName;
            vm.Status = status;
            vm.FromDate = fromDate;
            vm.ToDate = toDate;

            return vm;
        }
        public void Approve(int leaveId,int managerId,string? comment)
        {
            LeaveRequest leave = Context.LeaveRequests
        .FirstOrDefault(l =>
            l.Id == leaveId &&
            l.ApprovalManagerId == managerId);

            if (leave == null)
                return;

            if (leave.Status != "Pending")
                return;
            leave.Status = "Approved";
            leave.ManagerComment = comment;
            leave.DecisionDate = DateTime.Now;

            Context.SaveChanges();

        }
        public void Reject(int leaveId, int managerId, string? comment)
        {
            LeaveRequest leave = Context.LeaveRequests
                .FirstOrDefault(l =>
                    l.Id == leaveId &&
                    l.ApprovalManagerId == managerId);

            if (leave == null)
                return;

            if (leave.Status != "Pending")
                return;

            leave.Status = "Rejected";
            leave.ManagerComment = comment;
            leave.DecisionDate = DateTime.Now;

            Context.SaveChanges();
        }

    }
}
