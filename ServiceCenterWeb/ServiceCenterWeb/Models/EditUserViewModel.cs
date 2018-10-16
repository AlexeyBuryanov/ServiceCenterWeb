using System.ComponentModel.DataAnnotations;

namespace ServiceCenterWeb.Models
{
    public class EditUserViewModel
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }
    }
}
