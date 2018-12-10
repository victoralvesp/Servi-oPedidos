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

        public RepositorioDePedidos(ContextoPedidos contexto)
        {
            _contexto = contexto;
        }

        public async Task<IPedidoDTO> AdicionarPedidoAsync(IPedidoDTO pedido)
        {
            PedidoModeloBD pedidoBD = new PedidoModeloBD(pedido); 

            _contexto.Add(pedidoBD);
            _contexto.AddRange(pedidoBD.ItensBD.ToList());

            await _contexto.SaveChangesAsync();

            

            return pedidoBD;
        }

        public async Task<IPedidoDTO> AlterarPedidoAsync(IPedidoDTO pedido)
        {
            PedidoModeloBD pedidoBD = new PedidoModeloBD(pedido);

            _contexto.Update(pedidoBD);
            AtualizaItensDoPedido(pedidoBD);

            await _contexto.SaveChangesAsync();


            IPedidoDTO pedidoSalvo = await ObterPedidoAsync(pedidoBD.Id);

            
            return pedidoSalvo;
        }

        private void AtualizaItensDoPedido(PedidoModeloBD pedidoBD)
        {
            IEnumerable<ItemDePedidoModeloBD> itensBD = pedidoBD.ItensBD;
            int[] idsItens = itensBD.Select(item => item.Id).Where(id => id > 0).Distinct().ToArray();
            _contexto.UpdateRange(itensBD.Where(item => item.Id > 0).ToList());
            IQueryable<ItemDePedidoModeloBD> entities = _contexto.ItensDePedido.Where(item => item.IdPedido == pedidoBD.Id && !idsItens.Contains(item.Id));
            _contexto.RemoveRange(entities);
            _contexto.AddRange(itensBD.Where(item => item.Id <= 0).ToList());
        }

        public async Task<IClienteDTO> ObterClienteAsync(int idCliente)
        {
            ClienteModeloBD clienteBD = await _contexto.Clientes.FindAsync(idCliente);
           
            return clienteBD;
        }

        public async Task<IPedidoDTO> ObterPedidoAsync(int idPedido)
        {
            PedidoModeloBD pedidoDTO = await ((from pedido in _contexto.Pedidos.Include(x => x.ItensBD)
                                              where pedido.Id == idPedido
                                              select pedido).SingleAsync());


            return pedidoDTO;
        }

        public async Task<IEnumerable<IPedidoDTO>> ObterPedidosAsync()
        {
            IEnumerable<IPedidoDTO> pedidosDTO = await _contexto.Pedidos.Include(x => x.ItensBD).ToArrayAsync();
            
            return pedidosDTO;
        }

        public async Task<IProdutoDTO> ObterProdutoAsync(int idProduto)
        {
            ProdutoModeloBD produtoBD = await _contexto.Produtos.FindAsync(idProduto);
            
            return produtoBD;
        }

        public async Task<IEnumerable<IProdutoDTO>> ObterProdutosAsync(int[] idsProdutos)
        {
            IEnumerable<ProdutoModeloBD> produtosBD = await _contexto.Produtos.Where(pr => idsProdutos.Contains(pr.Id)).ToArrayAsync();

            return produtosBD;
        }

        public async Task<IEnumerable<IProdutoDTO>> ObterProdutosAsync()
        {
            IEnumerable<ProdutoModeloBD> produtosBD = await _contexto.Produtos.ToArrayAsync();

            return produtosBD;
        }

        public async Task<IEnumerable<IClienteDTO>> ObterClientesAsync()
        {
            IEnumerable<ClienteModeloBD> clientesBD = await _contexto.Clientes.ToArrayAsync();

            return clientesBD;
        }
    }
}
