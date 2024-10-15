using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarDotNetIdentityFramework.Data;
using StellarDotNetIdentityFramework.Models.Identity;

namespace StellarDotNetIdentityFramework.Areas.Identity.Pages.Account.Manage
{
    public class KeypairsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public KeypairsController(
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet("GetEncryptedKeypairs")]
        public async Task<IActionResult> GetEncryptedKeypairs()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var keypairs = await _context.UserKeyPairs
                .Where(kp => kp.UserId == user.Id)
                .Select(kp => new
                {
                    kp.Id,
                    kp.UserId,
                    kp.User,
                    kp.EncryptedKeyPair,
                    kp.PublicKey,
                    kp.Label,
                    kp.CreatedAt
                })
                .ToListAsync();

            return Ok(keypairs);
        }
    }
}

