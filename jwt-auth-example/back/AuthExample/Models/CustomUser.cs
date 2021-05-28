using Microsoft.AspNetCore.Identity;

namespace AuthExample.Models
{
    public class CustomUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
