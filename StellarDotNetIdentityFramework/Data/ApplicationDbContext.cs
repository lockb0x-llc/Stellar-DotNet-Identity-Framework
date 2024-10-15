using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StellarDotNetIdentityFramework.Models.Identity;


namespace StellarDotNetIdentityFramework.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUserKeyPair> UserKeyPairs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure the one-to-many relationship
            builder.Entity<ApplicationUserKeyPair>()
                .HasOne(kp => kp.User)
                .WithMany(static u => u.KeyPairs)
                .HasForeignKey(kp => kp.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Ensure GUIDs are used as primary keys
            builder.Entity<ApplicationUser>(b =>
            {
                b.HasKey(u => u.Id);
                b.Property(u => u.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<ApplicationRole>(b =>
            {
                b.HasKey(r => r.Id);
                b.Property(r => r.Id).ValueGeneratedOnAdd();
            });
        }
    }
}
