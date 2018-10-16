using Microsoft.AspNetCore.Identity;

namespace ServiceCenterWeb.Models
{
    public class User : IdentityUser
    {
        public string AvatarPath { get; set; }
    }
}
