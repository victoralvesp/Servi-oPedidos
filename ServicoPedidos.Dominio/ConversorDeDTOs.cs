using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public class ConversorDeDTOs : IConversorDeDTOs
    {
        IRepositorioDePedidos _repositorioDePedidos;

        public ConversorDeDTOs(IRepositorioDePedidos repositorioDePedidos)
        {
            _repositorioDePedidos = repositorioDePedidos;
        }

        public IPedidoDTO ConverterParaDTO(IPedido pedido)
        {
            IEnumerable<IItemDePedidoDTO> itensDTO = pedido.Itens.Select(item => ConverterParaDTO(item));

            IPedidoDTO pedidoDTO = new PedidoDTO(itensDTO)
            {
                Id = pedido.Id,
                IdCliente = pedido.Cliente.Id
            };

            return pedidoDTO;
        }

        public IItemDePedidoDTO ConverterParaDTO(IItemDePedido item)
        {
            IItemDePedidoDTO itemDePedidoDTO = new ItemDePedidoDTO
            {
                Id = item.Id,
                IdProduto = item.Produto.Id,
                Quantidade = item.Quantidade,
                PrecoUnitario = item.PrecoUnitario
            };

            return itemDePedidoDTO;
        }

        public IPedido ConverterPedidoDTO(IPedidoDTO pedidoDTO)
        {
            ICliente clienteDoPedido = _repositorioDePedidos.ObterCliente(pedidoDTO.IdCliente);
            IDictionary<int, IProduto> produtosDosItensPorId = ObterProdutosDoPedidoParaFacilAcesso(pedidoDTO);
            IEnumerable<IItemDePedido> itens = ConverteItensDoPedido(pedidoDTO, produtosDosItensPorId);

            IPedido pedido = new Pedido(clienteDoPedido, itens)
            {
                Id = pedidoDTO.Id
            };

            return pedido;
        }

        private IEnumerable<IItemDePedido> ConverteItensDoPedido(IPedidoDTO pedidoDTO, IDictionary<int, IProduto> produtosDosItensPorId)
        {
            return pedidoDTO.Itens.Select(itemDTO =>
            {
                IProduto produtoDoPedido = produtosDosItensPorId[itemDTO.IdProduto];
                IItemDePedido item = ConverteParaItem(itemDTO, produtoDoPedido);
                return item;
            });
        }

        private IDictionary<int, IProduto> ObterProdutosDoPedidoParaFacilAcesso(IPedidoDTO pedidoDTO)
        {
            IEnumerable<int> idsDosProdutosDosPedidos = pedidoDTO.Itens.Select(itemDTO => itemDTO.IdProduto).Distinct();

            IEnumerable<IProduto> produtosDosPedidos = _repositorioDePedidos.ObterProdutos(idsDosProdutosDosPedidos.ToArray());

            IDictionary<int, IProduto> produtosDosItensPorId = produtosDosPedidos.ToDictionary(produto => produto.Id);
            return produtosDosItensPorId;
        }

        public IItemDePedido ConverteParaItem(IItemDePedidoDTO itemDTO, IProduto produtoDoPedido)
        {
            IItemDePedido item = new ItemDePedido(produtoDoPedido, itemDTO.PrecoUnitario, itemDTO.Quantidade)
            {
                Id = itemDTO.Id
            };

            return item;
        }
    }
}
