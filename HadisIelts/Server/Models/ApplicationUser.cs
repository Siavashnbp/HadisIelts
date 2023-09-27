using HadisIelts.Server.Models.BaseModels;
using Microsoft.AspNetCore.Identity;

namespace HadisIelts.Server.Models
{
    public class ApplicationUser : IdentityUser, IEntity<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Skype { get; set; }
    }
}