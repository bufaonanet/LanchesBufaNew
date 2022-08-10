using LanchesBufaNew.Models;
using LanchesBufaNew.Repositories.Interfaces;
using LanchesBufaNew.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesBufaNew.Controllers;

public class LancheController : Controller
{
    private readonly ILancheRepository _lancheRepository;

    public LancheController(ILancheRepository lancheRepository)
    {
        _lancheRepository = lancheRepository;
    }

    public IActionResult List(string categoria)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(categoria))
        {
            lanches = _lancheRepository.Lanches.OrderBy(l => l.LancheId);
            categoriaAtual = "Todos os lanches";
        }
        else
        {
            lanches = _lancheRepository.Lanches
                    .Where(l => l.Categoria.CategoriaNome.Equals(categoria))
                    .OrderBy(l => l.Nome);

            categoriaAtual = categoria;
        }

        var lanchesListVM = new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        };
        return View(lanchesListVM);
    }

    public IActionResult Details(int lancheId)
    {
        var lanche = _lancheRepository.Lanches
            .FirstOrDefault(l => l.LancheId == lancheId);
        return View(lanche);
    }

    public IActionResult Search(string searchString)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(searchString))
        {
            lanches = _lancheRepository.Lanches.OrderBy(l => l.Nome);
            categoriaAtual = "Todos os Lanches";
        }
        else
        {
            lanches = _lancheRepository.Lanches
                .Where(l => l.Nome.ToLower().Contains(searchString.ToLower()))
                .OrderBy(l => l.Nome);

            if (lanches.Any())
                categoriaAtual = "Lanches";
            else
                categoriaAtual = "Nenhum lanche encontrado";
        }

        return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        });
    }
}
