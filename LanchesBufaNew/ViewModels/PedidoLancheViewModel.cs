using LanchesBufaNew.Models;

namespace LanchesBufaNew.ViewModels;

public class PedidoLancheViewModel
{
    public Pedido Pedido { get; set; }
    public IEnumerable<PedidoDetalhe> PedidoDetalhes { get; set; }
}