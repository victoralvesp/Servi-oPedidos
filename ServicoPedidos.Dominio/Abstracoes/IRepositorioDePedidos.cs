using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IRepositorioDePedidos
    {
        Task<IPedidoDTO> AdicionarPedidoAsync(IPedidoDTO pedido);
        Task<IClienteDTO> ObterClienteAsync(int idCliente);
        Task<IEnumerable<IClienteDTO>> ObterClientesAsync();
        Task<IEnumerable<IProdutoDTO>> ObterProdutosAsync();
        Task<IEnumerable<IProdutoDTO>> ObterProdutosAsync(int[] idsProdutos);
        Task<IPedidoDTO> AlterarPedidoAsync(IPedidoDTO pedido);
        Task<IProdutoDTO> ObterProdutoAsync(int idProduto);
        Task<IEnumerable<IPedidoDTO>> ObterPedidosAsync();
        Task<IPedidoDTO> ObterPedidoAsync(int idPedido);
    }
}
