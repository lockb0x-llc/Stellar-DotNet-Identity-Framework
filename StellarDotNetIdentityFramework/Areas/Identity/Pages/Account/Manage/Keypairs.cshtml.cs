using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using StellarDotNetIdentityFramework.Data;
using StellarDotNetIdentityFramework.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace StellarDotNetIdentityFramework.Areas.Identity.Pages.Account.Manage
{
    /// <summary>
    /// Model for managing user keypairs.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="KeypairsModel"/> class.
    /// </remarks>
    public class KeypairsModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : PageModel
    {

        /// <summary>
        /// List of the user's keypairs.
        /// </summary>
        public IList<ApplicationUserKeyPair> KeyPairs { get; set; } = [];

        [BindProperty]
        public InputModel Input { get; set; } = default!;

        [ViewData]
        public string ActivePage { get; set; } = default!;

        /// <summary>
        /// Handles GET requests to display the user's keypairs.
        /// </summary>
        public async Task<IActionResult> OnGetAsync()
        {
            ActivePage = ManageNavPages.ManageKeypairs;

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }
            KeyPairs = await context.UserKeyPairs
                .Where(kp => kp.UserId == user.Id)
                .ToListAsync();

            return Page();
        }

        /// <summary>
        /// Input model for adding a new keypair.
        /// </summary>
        public class InputModel
        {
            [Required]
            [Display(Name = "Keypair Label")]
            public string Label { get; set; } = default!;
        }
    }
}
