using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StellarDotNetIdentityFramework.Data;
using StellarDotNetIdentityFramework.Models.Identity;
using System.ComponentModel.DataAnnotations;

namespace StellarDotNetIdentityFramework.Controllers
{
    /// <summary>
    /// API Controller for managing user keypairs.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class KeypairsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IAntiforgery _antiforgery;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeypairsController"/> class.
        /// </summary>
#pragma warning disable IDE0290 // Use primary constructor
        public KeypairsController(
#pragma warning restore IDE0290 // Use primary constructor
            UserManager<ApplicationUser> userManager,
            ApplicationDbContext context,
            IAntiforgery antiforgery)
        {
            _userManager = userManager;
            _context = context;
            _antiforgery = antiforgery;
        }

        /// <summary>
        /// Retrieves the encrypted keypairs for the authenticated user.
        /// </summary>
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
                    kp.EncryptedSecret,
                    kp.PublicKey,
                    kp.Label,
                    kp.CreatedAt
                })
                .ToListAsync();

            return Ok(keypairs);
        }

        /// <summary>
        /// Adds a new keypair to the user's account.
        /// </summary>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] AddKeypairRequest request)
        {
            // Manually validate the antiforgery token
            try
            {
                await _antiforgery.ValidateRequestAsync(HttpContext);
            }
            catch (AntiforgeryValidationException)
            {
                return BadRequest("Antiforgery token validation failed.");
            }

            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            // Create and save the UserKeyPair
            var userKeyPair = new ApplicationUserKeyPair
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                User = user,
                EncryptedSecret = request.EncryptedSecret,
                PublicKey = request.PublicKey,
                Label = request.Label,
                CreatedAt = DateTime.UtcNow
            };

            _context.UserKeyPairs.Add(userKeyPair);
            await _context.SaveChangesAsync();

            return Ok(new { success = true });
        }

        /// <summary>
        /// Updates existing encrypted keypairs with re-encrypted secrets.
        /// </summary>
        [HttpPost("UpdateEncryptedKeypairs")]
        public async Task<IActionResult> UpdateEncryptedKeypairs([FromBody] UpdateKeypairsRequest request)
        {
            // Manually validate the antiforgery token
            try
            {
                await _antiforgery.ValidateRequestAsync(HttpContext);
            }
            catch (AntiforgeryValidationException)
            {
                return BadRequest("Antiforgery token validation failed.");
            }

            // Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            try
            {
                foreach (var reEncryptedKeypair in request.ReEncryptedKeypairs)
                {
                    var userKeypair = await _context.UserKeyPairs
                        .FirstOrDefaultAsync(kp => kp.Id == reEncryptedKeypair.Id && kp.UserId == user.Id);

                    if (userKeypair == null)
                    {
                        return BadRequest($"Keypair with ID {reEncryptedKeypair.Id} not found or does not belong to the user.");
                    }

                    // Update the encrypted secret
                    userKeypair.EncryptedSecret = reEncryptedKeypair.EncryptedSecret;
                    _context.UserKeyPairs.Update(userKeypair);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                return StatusCode(500, $"An error occurred while updating the keypairs - Message: {ex.Message}");
            }

            return Ok(new { success = true });
        }

        [HttpPost("VerifyMfaAndGetSecret")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyMfaAndGetSecret([FromBody] MfaRequestModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            var twoFactorCode = model.TwoFactorCode.Replace(" ", string.Empty).Replace("-", string.Empty);

            var isValid = await _userManager.VerifyTwoFactorTokenAsync(user, TokenOptions.DefaultAuthenticatorProvider, twoFactorCode);

            if (!isValid)
            {
                return BadRequest(new { message = "Invalid verification code." });
            }

            // MFA verification successful
            // Retrieve the encrypted secret
            var keypair = await _context.UserKeyPairs.FindAsync(Guid.Parse(model.KeypairId));

            if (keypair == null || keypair.UserId != user.Id)
            {
                return NotFound(new { message = "Keypair not found." });
            }

            return Ok(new { success = true, encryptedSecret = keypair.EncryptedSecret });
        }


    }

    /// <summary>
    /// Request model for adding a keypair.
    /// </summary>
    public class AddKeypairRequest
    {
        [Required]
        public string Label { get; set; } = default!;

        [Required]
        public string PublicKey { get; set; } = default!;

        [Required]
        public string EncryptedSecret { get; set; } = default!;
    }

    /// <summary>
    /// Request model for updating multiple re-encrypted keypairs.
    /// </summary>
    public class UpdateKeypairsRequest
    {
        [Required]
        public List<ReEncryptedKeypair> ReEncryptedKeypairs { get; set; } = default!;
    }

    /// <summary>
    /// Model representing a re-encrypted keypair.
    /// </summary>
    public class ReEncryptedKeypair
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string EncryptedSecret { get; set; } = default!;
    }

    public class MfaRequestModel
    {
        public string TwoFactorCode { get; set; } = default!;
        public string KeypairId { get; set; } = default!;
    }
}
