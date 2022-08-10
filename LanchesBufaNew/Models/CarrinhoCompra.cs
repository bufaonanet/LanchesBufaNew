using LanchesBufaNew.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesBufaNew.Models;

public class CarrinhoCompra
{
    private readonly AppDbContext _context;

    public CarrinhoCompra(AppDbContext context)
    {
        _context = context;
    }

    public string CarrinhoCompraId { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

    public static CarrinhoCompra GetCarrinho(IServiceProvider services)
    {
        //define uma sessão
        ISession session =
            services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        //obtem um serviço do tipo do nosso contexto 
        var context = services.GetService<AppDbContext>();

        //obtem ou gera o Id do carrinho
        string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        //atribui o id do carrinho na Sessão
        session.SetString("CarrinhoId", carrinhoId);

        //retorna o carrinho com o contexto e o Id atribuido ou obtido
        return new CarrinhoCompra(context)
        {
            CarrinhoCompraId = carrinhoId
        };
    }

    public void AdicionarAoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItens = _context.CarrinhoCompraItens
            .SingleOrDefault(c => c.Lanche.LancheId == lanche.LancheId &&
                                  c.CarrinhoCompraId == CarrinhoCompraId);

        if (carrinhoCompraItens is null)
        {
            carrinhoCompraItens = new CarrinhoCompraItem
            {
                CarrinhoCompraId = CarrinhoCompraId,
                Lanche = lanche,
                Quantidade = 1
            };
            _context.CarrinhoCompraItens.Add(carrinhoCompraItens);
        }
        else
        {
            carrinhoCompraItens.Quantidade++;
        }

        _context.SaveChanges();
    }

    public int RemoverDoCarrinho(Lanche lanche)
    {
        var carrinhoCompraItens = _context.CarrinhoCompraItens
            .SingleOrDefault(c => c.Lanche.LancheId == lanche.LancheId &&
                                  c.CarrinhoCompraId == CarrinhoCompraId);
        var quantidadeLocal = 0;


        if (carrinhoCompraItens != null)
        {
            if (carrinhoCompraItens.Quantidade > 1)
            {
                carrinhoCompraItens.Quantidade--;
                quantidadeLocal = carrinhoCompraItens.Quantidade;
            }
            else
            {
                _context.CarrinhoCompraItens.Remove(carrinhoCompraItens);
            }
        }
        _context.SaveChanges();
        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
    {
        return CarrinhoCompraItens ??
            (CarrinhoCompraItens = _context.CarrinhoCompraItens
                                           .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
                                           .Include(c => c.Lanche)
                                           .ToList());
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _context.CarrinhoCompraItens
            .Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

        _context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _context.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        var total = _context.CarrinhoCompraItens
            .Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
            .Select(c => c.Lanche.Preco * c.Quantidade)
            .Sum();

        return total;
    }
}