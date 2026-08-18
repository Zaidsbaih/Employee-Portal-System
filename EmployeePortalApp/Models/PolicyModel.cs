using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.Models
{
    public class PolicyModel
    {
        
            
            public int Id { get; set; }

          
            public string Title { get; set; }

            
            public string Category { get; set; }

            public string? Description { get; set; }

            
            public string? FilePath { get; set; }

            public IFormFile? PdfFile { get; set; }

            public DateTime UploadDate { get; set; }

            public bool IsActive { get; set; }
        
    }
}
