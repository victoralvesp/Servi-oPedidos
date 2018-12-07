using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IPedido> ConverterParaPedidoAsync(IPedidoDTO pedidoDTO)
        {
            ICliente clienteDoPedido = await _repositorioDePedidos.ObterClienteAsync(pedidoDTO.IdCliente);
            IDictionary<int, IProduto> produtosDosItensPorId = await ObterProdutosDoPedidoParaFacilAcessoAsync(pedidoDTO);
            IEnumerable<IItemDePedido> itens = await ConverteItensDoPedido(pedidoDTO, produtosDosItensPorId);

            IPedido pedido = new Pedido(clienteDoPedido, itens)
            {
                Id = pedidoDTO.Id
            };

            return pedido;
        }

        public async Task<IItemDePedido> ConverteParaItemAsync(IItemDePedidoDTO itemDTO)
        {
            IProduto produto = await _repositorioDePedidos.ObterProdutoAsync(itemDTO.IdProduto);

            IItemDePedido item = ConverteParaItem(itemDTO, produto);

            return item;
        }
        private IItemDePedido ConverteParaItem(IItemDePedidoDTO itemDTO, IProduto produtoDoPedido)
        {
            IItemDePedido item = new ItemDePedido(produtoDoPedido, itemDTO.PrecoUnitario, itemDTO.Quantidade)
            {
                Id = itemDTO.Id
            };

            return item;
        }

        private async Task<IEnumerable<IItemDePedido>> ConverteItensDoPedido(IPedidoDTO pedidoDTO, IDictionary<int, IProduto> produtosDosItensPorId)
        {
            return await Task.Run(() => pedidoDTO.Itens.Select(itemDTO =>
            {
                IProduto produtoDoPedido = produtosDosItensPorId[itemDTO.IdProduto];
                IItemDePedido item = ConverteParaItem(itemDTO, produtoDoPedido);
                return item;
            }));
        }

        private async Task<IDictionary<int, IProduto>> ObterProdutosDoPedidoParaFacilAcessoAsync(IPedidoDTO pedidoDTO)
        {
            IEnumerable<int> idsDosProdutosDosPedidos = pedidoDTO.Itens.Select(itemDTO => itemDTO.IdProduto).Distinct();

            IEnumerable<IProduto> produtosDosPedidos = await _repositorioDePedidos.ObterProdutosAsync(idsDosProdutosDosPedidos.ToArray());

            IDictionary<int, IProduto> produtosDosItensPorId = produtosDosPedidos.ToDictionary(produto => produto.Id);
            return produtosDosItensPorId;
        }

        public ICliente ConverterParaCliente(IClienteDTO clienteBD)
        {
            throw new NotImplementedException();
        }

        public IProduto ConverterParaProduto(IProdutoDTO produtoBD)
        {
            throw new NotImplementedException();
        }
    }
}
