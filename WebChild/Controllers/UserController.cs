using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using WebChild.Areas.Identity.Data;
using WebChild.Data;
using WebChild.Models;

namespace WebChild.Controllers;

public class UserController : Controller
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;
    
    private readonly AppDbContext _context;
    public const string CARTKEY = "cart";
    void ClearCart () {
        var session = HttpContext.Session;
        session.Remove (CARTKEY);
    }

    public UserController(AppDbContext context,
        SignInManager<AppUser> signInManager,
        UserManager<AppUser> userManager)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    // // GET
    // public IActionResult Index()
    // {
    //     return View();
    // }
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel usermodel)
    {
       // String returnUrl = Url.Content("~/");
       String returnUrl = "";
        // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        if (ModelState.IsValid)
        {
            var user = CreateUser();

            user.FirstName = usermodel.FristName;
            
            user.LastName = usermodel.LastName;

            user.UserName = usermodel.Email;
            
            user.PhoneNumber = usermodel.PhoneNumber;

            user.Address = usermodel.Address;

            // await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
            // await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, usermodel.Password);

            if (result.Succeeded)
            {
                // _logger.LogInformation("User created a new account with password.");
                //
                // var userId = await _userManager.GetUserIdAsync(user);
                // var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                // // ma hoa code gui mail
                // code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                // var callbackUrl = Url.Page(
                //     "/Account/ConfirmEmail",
                //     pageHandler: null,
                //     values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                //     protocol: Request.Scheme);

                // await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //     $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                if (_userManager.Options.SignIn.RequireConfirmedAccount)
                {
                    return RedirectToPage("RegisterConfirmation", new { email = usermodel.Email, returnUrl = returnUrl });
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index","Home");
                }
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }
        return View();
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel usermodel)
    {
        string returnUrl ="/Home";

        // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(usermodel.Email, usermodel.Password, usermodel.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }
            // if (result.RequiresTwoFactor)
            // {
            //     return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
            // }
            // if (result.IsLockedOut)
            // {
            //     _logger.LogWarning("User account locked out.");
            //     return RedirectToPage("./Lockout");
            // }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View();
            }
        }
        return View();
    }
    public IActionResult Logout()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Logout(string returnUrl = null)
    {
        await _signInManager.SignOutAsync();
        // _logger.LogInformation("User logged out.");
        if (returnUrl != null)
        {
            ClearCart();
            return LocalRedirect(returnUrl);
        }
        else
        {
            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return View();
        }

    }
    
    private AppUser CreateUser()
    {
        try
        {
            return Activator.CreateInstance<AppUser>();
        }
        catch
        {
            throw new InvalidOperationException($"Can't create an instance of '{nameof(AppUser)}'. " +
                                                $"Ensure that '{nameof(AppUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                                                $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            
        }
    }

}
