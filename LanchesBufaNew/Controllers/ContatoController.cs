using Microsoft.AspNetCore.Mvc;

namespace LanchesBufaNew.Controllers;

public class ContatoController : Controller
{
    public IActionResult Index()
    {
        return View();
        //if (User.Identity.IsAuthenticated)
        //{
        //    return View();
        //}
        //return RedirectToAction("Login", "Account");
    }
}