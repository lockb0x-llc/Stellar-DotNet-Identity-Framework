using Microsoft.AspNetCore.Identity;

namespace StellarDotNetIdentityFramework.Models.Identity
    {
    /// <summary>
    /// ApplicationUser class that inherits from ApplicationUser<Guid> and adds additional properties
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        public virtual required ICollection<ApplicationUserKeyPair> KeyPairs { get; set; }
    }
}


