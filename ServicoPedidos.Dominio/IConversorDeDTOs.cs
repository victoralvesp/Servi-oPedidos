using ServicoPedidos.Dominio.Abstracoes;

namespace ServicoPedidos.Dominio
{
    public interface IConversorDeDTOs
    {
        IItemDePedido ConverteParaItem(IItemDePedidoDTO itemDTO, IProduto produtoDoPedido);
        IItemDePedidoDTO ConverterParaDTO(IItemDePedido item);
        IPedidoDTO ConverterParaDTO(IPedido pedido);
        IPedido ConverterPedidoDTO(IPedidoDTO pedidoDTO);
    }
}