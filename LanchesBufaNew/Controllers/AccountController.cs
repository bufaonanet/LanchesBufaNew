using LanchesBufaNew.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LanchesBufaNew.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult Login(string returnUrl)
    {
        return View(new LoginViewModel
        {
            ReturnUrl = returnUrl
        });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid)
            return View(loginVM);

        var user = await _userManager.FindByNameAsync(loginVM.UserName);
        if (user != null)
        {
            var result = await _signInManager
                .PasswordSignInAsync(user, loginVM.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(loginVM.ReturnUrl))
                    return RedirectToAction("Index", "Home");

                return Redirect(loginVM.ReturnUrl);
            }
        }

        ModelState.AddModelError("", "Falha ao realizar o login");
        return View(loginVM);
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid)
            return View(loginVM);

        var user = new IdentityUser { UserName = loginVM.UserName };
        var result = await _userManager.CreateAsync(user, loginVM.Password);

        if (result.Succeeded)
        {
            return RedirectToAction(nameof(Login));
        }

        ModelState.AddModelError("Registro", "Falah ao realizar o registro");
        return View(loginVM);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.User = null;
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}
