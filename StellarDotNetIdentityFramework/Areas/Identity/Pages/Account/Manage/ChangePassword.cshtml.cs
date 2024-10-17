using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StellarDotNetIdentityFramework.Data;
using StellarDotNetIdentityFramework.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace StellarDotNetIdentityFramework.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Model for handling password changes and re-encrypting keypairs.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ChangePasswordModel"/> class.
    /// </remarks>
    public class ChangePasswordModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context,
        ILogger<ChangePasswordModel> logger) : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = default!;

        [TempData]
        public string StatusMessage { get; set; } = default!;

        /// <summary>
        /// Input model for changing password.
        /// </summary>
        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Current password")]
            public string OldPassword { get; set; } = default!;

            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "New password")]
            public string NewPassword { get; set; } = default!;

            [DataType(DataType.Password)]
            [Display(Name = "Confirm new password")]
            [Compare("NewPassword")]
            public string ConfirmPassword { get; set; } = default!;
        }

        /// <summary>
        /// Handles GET requests.
        /// </summary>
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");

            var hasPassword = await userManager.HasPasswordAsync(user);
            if (!hasPassword)
                return RedirectToPage("./SetPassword");

            return Page();
        }

        /// <summary>
        /// Handles POST requests to change the password and re-encrypt keypairs.
        /// </summary>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await userManager.GetUserAsync(User);
            if (user == null)
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");

            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var changePasswordResult = await userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);

                    return Page();
                }

                // No need to handle re-encrypted keypairs here since it's done via AJAX

                await transaction.CommitAsync();

                await signInManager.RefreshSignInAsync(user);
                logger.LogInformation("User changed their password successfully.");
                StatusMessage = "Your password has been changed.";

                return RedirectToPage();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                logger.LogError(ex, "An error occurred while changing the password.");
                ModelState.AddModelError(string.Empty, "An error occurred while changing your password. Please try again.");
                return Page();
            }
        }
    }
}
