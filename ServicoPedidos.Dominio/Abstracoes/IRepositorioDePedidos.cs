using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IRepositorioDePedidos
    {
        IPedido AdicionarPedido(IPedido pedido);
        ICliente ObterCliente(int idCliente);
        IEnumerable<IProduto> ObterProdutos(int[] v);
        IPedido AlterarPedido(IPedido pedido);
    }
}
