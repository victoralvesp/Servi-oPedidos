using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoPedidos.Dominio
{
    public class DiretorDeRequisicoesDePedidos : IDiretorDeRequisicoesDePedidos
    {
        IConversorDeDTOs _conversor;
        IRepositorioDePedidos _repositorioDePedidos;

        public DiretorDeRequisicoesDePedidos(IConversorDeDTOs conversor, IRepositorioDePedidos repositorioDePedidos)
        {
            _conversor = conversor;
            _repositorioDePedidos = repositorioDePedidos;
        }

        public async Task<IPedidoDTO> InserirNovoPedidoAsync(IPedidoDTO pedidoDTO)
        {
            IPedido pedido = await _conversor.ConverterParaPedidoAsync(pedidoDTO);

            pedido.Validar();

            IPedidoDTO pedidoSalvo = await _repositorioDePedidos.AdicionarPedidoAsync(pedidoDTO);
            
            return pedidoSalvo;
        }

        public async Task<IPedidoDTO> AlterarPedidoAsync(IPedidoDTO pedidoDTO)
        {
            IPedido pedido = await _conversor.ConverterParaPedidoAsync(pedidoDTO);

            pedido.Validar();

            IPedidoDTO pedidoSalvo = await _repositorioDePedidos.AlterarPedidoAsync(pedidoDTO);

            return pedidoSalvo;
        }

        public async Task<IPedidoDTO> ObterPedidoAsync(int id)
        {
            return await _repositorioDePedidos.ObterPedidoAsync(id);
        }
    }
}
