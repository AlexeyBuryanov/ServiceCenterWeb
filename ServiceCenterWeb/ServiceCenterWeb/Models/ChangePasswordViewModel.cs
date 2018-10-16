using System.ComponentModel.DataAnnotations;

namespace ServiceCenterWeb.Models
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Key]
        public string Id { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Новый пароль")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Старый пароль")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
    }
}
