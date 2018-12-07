using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IRepositorioDePedidos
    {
        Task<IPedido> AdicionarPedido(IPedido pedido);
        Task<ICliente> ObterClienteAsync(int idCliente);
        Task<IEnumerable<IProduto>> ObterProdutosAsync(int[] idsProdutos);
        Task<IPedido> AlterarPedidoAsync(IPedido pedido);
        Task<IProduto> ObterProdutoAsync(int idProduto);
    }
}
