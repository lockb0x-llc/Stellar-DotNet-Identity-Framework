using StellarDotNetIdentityFramework.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class ApplicationUserKeyPair
{
    [Key]
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required ApplicationUser User { get; set; }

    public required string EncryptedKeyPair { get; set; }
    public required string PublicKey { get; set; }

    public required string Label { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
