using ServicoPedidos.Dominio.Abstracoes;
using System.Threading.Tasks;

namespace ServicoPedidos.Dominio
{
    public interface IDiretorDeRequisicoesDePedidos
    {
        Task<IPedidoDTO> AlterarPedido(IPedidoDTO pedidoDTO);
        Task<IPedidoDTO> InserirNovoPedido(IPedidoDTO pedidoDTO);
    }
}