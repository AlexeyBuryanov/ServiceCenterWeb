using System.ComponentModel.DataAnnotations;

namespace ServiceCenterWeb.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
