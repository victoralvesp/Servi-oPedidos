using ServicoPedidos.Dominio.Abstracoes;
using System.Threading.Tasks;

namespace ServicoPedidos.Dominio
{
    public interface IDiretorDeRequisicoesDePedidos
    {
        Task<IPedidoDTO> AlterarPedidoAsync(IPedidoDTO pedidoDTO);
        Task<IPedidoDTO> InserirNovoPedidoAsync(IPedidoDTO pedidoDTO);
        Task<IPedidoDTO> ObterPedidoAsync(int id);
    }
}