using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using StellarDotNetIdentityFramework.Data;
using StellarDotNetIdentityFramework.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace StellarDotNetIdentityFramework.Areas.Identity.Pages.Account.Manage
{

    public class KeypairsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public KeypairsModel(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
            KeyPairs = new List<ApplicationUserKeyPair>();
        }

        public IList<ApplicationUserKeyPair> KeyPairs { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            KeyPairs = await _context.UserKeyPairs
                .Where(kp => kp.UserId == user.Id)
                .ToListAsync();

            return Page();
        }
    }
}
