﻿using LanchesBufaNew.Context;
using LanchesBufaNew.Models;
using LanchesBufaNew.Repositories.Interfaces;

namespace LanchesBufaNew.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;
    private readonly CarrinhoCompra _carrinhoCompra;

    public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
    {
        _context = context;
        _carrinhoCompra = carrinhoCompra;
    }

    public void CriarPedido(Pedido pedido)
    {
        pedido.PedidoEnviado = DateTime.Now;
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var carrinhoCompraItens = _carrinhoCompra.GetCarrinhoCompraItens();
        foreach (var item in carrinhoCompraItens)
        {
            PedidoDetalhe pedidoDetalhe = new()
            {
                Quantidade = item.Quantidade,
                LancheId = item.Lanche.LancheId,
                PedidoId = pedido.PedidoId,
                Preco = item.Lanche.Preco
            };
            _context.PedidoDetalhes.Add(pedidoDetalhe);
        }
        _context.SaveChanges();
    }
}
