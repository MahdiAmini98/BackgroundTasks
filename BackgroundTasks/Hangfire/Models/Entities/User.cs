using Microsoft.AspNetCore.Identity;

namespace Hangfire.Models.Entities
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
    }
}
