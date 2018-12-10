using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Rentabilidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicoPedidos.Dominio
{
    public interface IDiretorDeRequisicoesDePedidos
    {
        Task<IPedidoDTO> AlterarPedidoAsync(IPedidoDTO pedidoDTO);
        Task<IPedidoDTO> InserirNovoPedidoAsync(IPedidoDTO pedidoDTO);
        Task<IPedidoDTO> ObterPedidoAsync(int id);
        Task<Rentabilidade> CalcularRentabilidade(ValorMonetario precoSugerido, ValorMonetario precoUnitario);
        Task<IEnumerable<IClienteDTO>> ObterClientes();
        Task<IEnumerable<IProdutoDTO>> ObterProdutos();
    }
}