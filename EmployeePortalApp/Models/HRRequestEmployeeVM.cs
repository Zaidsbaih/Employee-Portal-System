namespace EmployeePortalApp.Models
{
    public class HRRequestEmployeeVM
    {
        public HRRequestModel NewRequest { get; set; }
            = new HRRequestModel();

        public List<HRRequestModel> MyRequests { get; set; }
            = new List<HRRequestModel>();
    }
}
