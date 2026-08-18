using System.ComponentModel.DataAnnotations;

namespace EmployeePortalApp.Models
{
    public class SetPasswordModel
    {
        public string Token { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
