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

        public async Task<IPedidoDTO> InserirNovoPedido(IPedidoDTO pedidoDTO)
        {
            IPedido pedido = await _conversor.ConverterParaPedidoAsync(pedidoDTO);

            pedido.Validar();

            IPedido pedidoSalvo = await _repositorioDePedidos.AdicionarPedido(pedido);

            IPedidoDTO pedidoDTOparaRetorno = _conversor.ConverterParaDTO(pedido);
            return pedidoDTOparaRetorno;
        }

        public async Task<IPedidoDTO> AlterarPedido(IPedidoDTO pedidoDTO)
        {
            IPedido pedido = await _conversor.ConverterParaPedidoAsync(pedidoDTO);

            pedido.Validar();

            IPedido pedidoSalvo = await _repositorioDePedidos.AlterarPedidoAsync(pedido);

            IPedidoDTO pedidoDTOparaRetorno = _conversor.ConverterParaDTO(pedido);
            return pedidoDTOparaRetorno;
        }

    }
}
