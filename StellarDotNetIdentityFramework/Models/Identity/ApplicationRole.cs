using Microsoft.AspNetCore.Identity;
using System;

namespace StellarDotNetIdentityFramework.Models.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        // Additional properties can be added here
        public required string Description { get; set; }
    }
}
