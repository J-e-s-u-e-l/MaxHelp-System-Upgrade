using MaxHelp_System_Upgrade.Models;
using MaxHelp_System_Upgrade.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MaxHelp_System_Upgrade.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet] 
        public IActionResult Login()
        {
            ViewBag.Stylesheet = "~/css/login.css";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.IsCompletedSuccessfully)
                {
                    // Retrieve user and their Business Unit identifier
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    // Store Business Unit identifier in session
                    if (user == null) { return  View("User does not exist!"); }
                    HttpContext.Session.SetString("BUIdentifier", user.BusinessUnitId.ToString());

                    return RedirectToAction("Index", "Dashboard");
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt!");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
