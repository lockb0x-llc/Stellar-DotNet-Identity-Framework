using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace StellarDotNetIdentityFramework.Models.Identity
{
    /// <summary>
    /// Model for storing a user's keypair.
    /// </summary>
    [Table("UserKeyPairs")]
    [Index(nameof(UserId), nameof(PublicKey), IsUnique = true)]
    public class ApplicationUserKeyPair
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser User { get; set; }

        public required string EncryptedSecret { get; set; }
        public required string PublicKey { get; set; }

        public required string Label { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
