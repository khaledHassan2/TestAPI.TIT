using Microsoft.AspNetCore.Identity;

namespace TestAPI.TIT.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
