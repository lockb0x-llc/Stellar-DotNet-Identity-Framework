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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configure Asp Net Identity Tables to use Guid
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
