using EmployeePortalApp.Models;

namespace EmployeePortalApp.Servicies
{
    public interface ILeaveServices
    {
        void SubmitLeave(int id, LeaveRequestModel model);
        LeaveRequestVM GetLeaveRequestPage(int employeeId);
        LeaveRequestModel Load(int id);

        void Update(LeaveRequestModel model);

        void Cancel(int id);

        bool IsManager(int employeeId);

        ApprovalRequestsVM GetApprovalRequests(
            int managerId,
            string? employeeName,
            string? status,
            DateTime? fromDate,
            DateTime? toDate);

        void Approve(int leaveId, int managerId, string? comment);

        void Reject(int leaveId, int managerId, string? comment);
    }
}
