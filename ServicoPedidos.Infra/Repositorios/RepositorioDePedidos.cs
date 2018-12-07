using Microsoft.EntityFrameworkCore;
using ServicoPedidos.Dominio;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Infra.Contextos;
using ServicoPedidos.Infra.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoPedidos.Infra.Repositorios
{
    public class RepositorioDePedidos : IRepositorioDePedidos
    {
        private ContextoPedidos _contexto;
        private IConversorDeDTOs _conversor;

        public RepositorioDePedidos(ContextoPedidos contexto, IConversorDeDTOs conversor)
        {
            _contexto = contexto;
            _conversor = conversor;
        }

        public Task<IPedido> AdicionarPedido(IPedido pedido)
        {
            throw new NotImplementedException();
        }

        public async Task<IPedido> AdicionarPedidoAsync(IPedido pedido)
        {
            PedidoModeloBD pedidoBD = new PedidoModeloBD(pedido); 

            _contexto.Add(pedidoBD);
            _contexto.AddRange(pedidoBD.ItensBD);

            await _contexto.SaveChangesAsync();

            IPedido pedidoSalvo = await _conversor.ConverterParaPedidoAsync(pedidoBD);

            return pedidoSalvo;
        }

        public async Task<IPedido> AlterarPedidoAsync(IPedido pedido)
        {
            PedidoModeloBD pedidoBD = new PedidoModeloBD(pedido);

            _contexto.Update(pedidoBD);
            _contexto.UpdateRange(pedidoBD.ItensBD);

            await _contexto.SaveChangesAsync();

            IPedido pedidoSalvo = await _conversor.ConverterParaPedidoAsync(pedidoBD);

            return pedidoSalvo;
        }

        public async Task<ICliente> ObterClienteAsync(int idCliente)
        {
            ClienteModeloBD clienteBD = await _contexto.Clientes.FindAsync(idCliente);

            ICliente cliente = _conversor.ConverterParaCliente(clienteBD);

            return cliente;
        }

        public async Task<IProduto> ObterProdutoAsync(int idProduto)
        {
            ProdutoModeloBD produtoBD = await _contexto.Produtos.FindAsync(idProduto);

            IProduto produto = _conversor.ConverterParaProduto(produtoBD);

            return produto;
        }

        public async Task<IEnumerable<IProduto>> ObterProdutosAsync(int[] idsProdutos)
        {
            IEnumerable<ProdutoModeloBD> produtosBD = await _contexto.Produtos.ToArrayAsync();

            IEnumerable<IProduto> produtos = produtosBD.Select(produtoBD => _conversor.ConverterParaProduto(produtoBD));

            return produtos;
        }
    }
}
