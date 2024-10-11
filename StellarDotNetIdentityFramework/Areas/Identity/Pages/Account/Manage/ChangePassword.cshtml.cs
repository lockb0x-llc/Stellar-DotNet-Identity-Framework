using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StellarDotNetIdentityFramework.Data;
using StellarDotNetIdentityFramework.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

public class ChangePasswordModel : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ChangePasswordModel> _logger;

    public ChangePasswordModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ApplicationDbContext context,
        ILogger<ChangePasswordModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _context = context;
        _logger = logger;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    [BindProperty]
    public string ReEncryptedKeypairs { get; set; }

    [TempData]
    public string StatusMessage { get; set; }

    public class InputModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public required string OldPassword { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public required string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword")]
        public required string ConfirmPassword { get; set; }
    }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        var hasPassword = await _userManager.HasPasswordAsync(user);
        if (!hasPassword)
            return RedirectToPage("./SetPassword");

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            return Page();

        var user = await _userManager.GetUserAsync(User);
        if (user == null)
            return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var changePasswordResult = await _userManager.ChangePasswordAsync(user, Input.OldPassword, Input.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return Page();
            }

            if (!string.IsNullOrEmpty(ReEncryptedKeypairs))
            {
                var reEncryptedKeypairs = JsonConvert.DeserializeObject<List<ReEncryptedKeypairModel>>(ReEncryptedKeypairs);

                foreach (var reEncryptedKeypair in reEncryptedKeypairs)
                {
                    var userKeypair = await _context.UserKeyPairs.FirstOrDefaultAsync(kp => kp.Id == reEncryptedKeypair.Id && kp.UserId == user.Id);
                    if (userKeypair != null)
                    {
                        userKeypair.EncryptedKeyPair = reEncryptedKeypair.EncryptedKeyPair;
                        _context.UserKeyPairs.Update(userKeypair);
                    }
                }

                await _context.SaveChangesAsync();
            }

            await transaction.CommitAsync();

            await _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("User changed their password successfully.");
            StatusMessage = "Your password has been changed.";

            return RedirectToPage();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            _logger.LogError(ex, "An error occurred while changing the password and updating keypairs.");
            ModelState.AddModelError(string.Empty, "An error occurred while changing your password. Please try again.");
            return Page();
        }
    }

    public class ReEncryptedKeypairModel
    {
        public Guid Id { get; set; }
        public required string EncryptedKeyPair { get; set; }
    }
}
