namespace EmployeePortalApp.Models
{
    public class LeaveRequestVM
    {
        public LeaveRequestModel LeaveRequest { get; set; }
        = new LeaveRequestModel();

        public List<LeaveRequestModel> MyLeaves { get; set; }
            = new List<LeaveRequestModel>();
    }
}
