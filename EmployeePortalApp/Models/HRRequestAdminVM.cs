namespace EmployeePortalApp.Models
{
    public class HRRequestAdminVM
    {
        public List<HRRequestModel> Requests { get; set; }
        = new List<HRRequestModel>();

        public string? EmployeeName { get; set; }

        public string? RequestType { get; set; }

        public string? Status { get; set; }
    }
}
