using Microsoft.AspNetCore.Mvc;

namespace LanchesBufaNew.Controllers;

public class ContatoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}