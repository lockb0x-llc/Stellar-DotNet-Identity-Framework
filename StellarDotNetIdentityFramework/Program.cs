using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StellarDotNetIdentityFramework.Data;
using StellarDotNetIdentityFramework.Models.Identity;

/// <summary>
/// The main entry point for the application.
/// </summary>
public class Program
{
    /// <summary>
    /// The main method which is the entry point of the application.
    /// </summary>
    /// <param name="args">An array of command-line arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        // Add the database context to the service container
        // Default is to use PostgreSQL
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        // Add Identity services using custom Applicationuser for the user and custom ApplicationRole for the role
        // and custom ApplicationDbContext for the database context
        //Adds token provider support for the default two-factor authentication types as well as for third party authenticator apps.
        _ = builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Tokens.AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider;
            })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

        // Register UserManager and RoleManager services
        builder.Services.AddScoped<UserManager<ApplicationUser>>();
        builder.Services.AddScoped<RoleManager<ApplicationRole>>();
        builder.Services.AddScoped<SignInManager<ApplicationUser>>();
        builder.Services.AddScoped<IUserStore<ApplicationUser>, UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>();
        builder.Services.AddScoped<IRoleStore<ApplicationRole>, RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();

        builder.Services.AddRazorPages();

        builder.Services.AddAntiforgery(options =>
        {
            options.HeaderName = "RequestVerificationToken";
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();
        app.MapRazorPages();

        app.Run();
    }
}
