namespace EmployeePortalApp.Models
{
    public class HRRequestModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public string RequestType { get; set; } = "";

        public string? Description { get; set; }

        public string Status { get; set; } = "";

        public string? HRComment { get; set; }

        public string? AttachmentPath { get; set; }

        public IFormFile? AttachmentFile { get; set; }

        public DateTime RequestDate { get; set; }

        public DateTime? CompletedDate { get; set; }

        // للعرض عند HR
        public string? EmployeeName { get; set; }
    }
}
