using Microsoft.AspNetCore.Identity;

namespace CRUDEFCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
