namespace EmployeePortalApp.Models
{
    public class ApprovalRequestsVM
    {
        public List<LeaveRequestModel> Requests { get; set; }
        = new List<LeaveRequestModel>();

        public string? EmployeeName { get; set; }

        public string? Status { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

    }
}
