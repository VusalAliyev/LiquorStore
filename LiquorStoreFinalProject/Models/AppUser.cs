using Microsoft.AspNetCore.Identity;

namespace LiquorStoreFinalProject.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
