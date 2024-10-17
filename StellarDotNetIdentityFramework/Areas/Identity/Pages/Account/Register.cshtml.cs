using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StellarDotNetIdentityFramework.Data;
using StellarDotNetIdentityFramework.Models.Identity;

namespace StellarDotNetIdentityFramework.Areas.Identity.Pages.Account
{
    /// <summary>
    /// Model for user registration.
    /// </summary>
    public class RegisterModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterModel"/> class.
        /// </summary>
#pragma warning disable IDE0290 // Use primary constructor
        public RegisterModel(
#pragma warning restore IDE0290 // Use primary constructor
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        public string ReturnUrl { get; set; } = default!;

        public IList<AuthenticationScheme> ExternalLogins { get; set; } = default!;

        /// <summary>
        /// Handles GET requests.
        /// </summary>
        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        /// <summary>
        /// Handles POST requests to register a new user.
        /// </summary>
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            
            if (ModelState.IsValid)
            {
                
                var keyPair = new ApplicationUserKeyPair
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    EncryptedSecret = Input.EncryptedSecret,
                    PublicKey = Input.PublicKey,
                    Label = "Initial Account Key",
                    CreatedAt = DateTime.UtcNow
                };
                              

                var user = new ApplicationUser
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    KeyPairs = [keyPair]
                };

                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        // Handle email confirmation
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        /// <summary>
        /// Input model for user registration.
        /// </summary>
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; } = default!;

            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = default!;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password")]
            public string ConfirmPassword { get; set; } = default!;

            public string EncryptedSecret { get; set; } = default!;

            public string PublicKey { get; set; } = default!;
        }
    }
}
