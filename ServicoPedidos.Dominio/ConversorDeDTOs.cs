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
            IClienteDTO clienteDoPedidoDTO = await _repositorioDePedidos.ObterClienteAsync(pedidoDTO.IdCliente);
            ICliente clienteDoPedido = ConverterParaCliente(clienteDoPedidoDTO);
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
            IProdutoDTO produtoDTO = await _repositorioDePedidos.ObterProdutoAsync(itemDTO.IdProduto);

            IProduto produto = ConverterParaProduto(produtoDTO);

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

            IEnumerable<IProdutoDTO> produtosDoPedidosDTO = await _repositorioDePedidos.ObterProdutosAsync(idsDosProdutosDosPedidos.ToArray());

            IEnumerable<IProduto> produtosDoPedido = produtosDoPedidosDTO.Select(produto => ConverterParaProduto(produto));

            IDictionary<int, IProduto> produtosDosItensPorId = produtosDoPedido.ToDictionary(produto => produto.Id);
            return produtosDosItensPorId;
        }

        public ICliente ConverterParaCliente(IClienteDTO clienteDTO)
        {
            return new Cliente(clienteDTO.Nome) { Id = clienteDTO.Id };
        }

        public IProduto ConverterParaProduto(IProdutoDTO produtoDTO)
        {
            int multiplo = produtoDTO.Multiplo ?? 1;
            return new Produto(produtoDTO.Nome, produtoDTO.PrecoSugerido, multiplo) { Id = produtoDTO.Id };
        }

        public async Task<IEnumerable<IPedido>> ConverterParaPedidosAsync(IEnumerable<IPedidoDTO> pedidosDTO)
        {
            List<Task<IPedido>> tasks = new List<Task<IPedido>>();

            foreach (var pedidoDTO in pedidosDTO)
            {
                tasks.Add(ConverterParaPedidoAsync(pedidoDTO));
            }

            await Task.WhenAll(tasks);

            return tasks.Select(task => task.Result).ToArray();
        }
    }
}
