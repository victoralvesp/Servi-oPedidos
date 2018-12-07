using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public class DiretorDeRequisicoesDePedidos
    {
        IConversorDeDTOs _conversor;
        IRepositorioDePedidos _repositorioDePedidos;

        public DiretorDeRequisicoesDePedidos(IConversorDeDTOs conversor, IRepositorioDePedidos repositorioDePedidos)
        {
            _conversor = conversor;
            _repositorioDePedidos = repositorioDePedidos;
        }

        public IPedidoDTO InserirNovoPedido(IPedidoDTO pedidoDTO)
        {
            IPedido pedido = _conversor.ConverterPedidoDTO(pedidoDTO);

            pedido.Validar();

            IPedido pedidoSalvo = _repositorioDePedidos.AdicionarPedido(pedido);

            IPedidoDTO pedidoDTOparaRetorno = _conversor.ConverterParaDTO(pedido);
            return pedidoDTOparaRetorno;
        }

        public IPedidoDTO AlterarPedido(IPedidoDTO pedidoDTO)
        {
            IPedido pedido = _conversor.ConverterPedidoDTO(pedidoDTO);

            pedido.Validar();

            IPedido pedidoSalvo = _repositorioDePedidos.AlterarPedido(pedido);

            IPedidoDTO pedidoDTOparaRetorno = _conversor.ConverterParaDTO(pedido);
            return pedidoDTOparaRetorno;
        }

    }
}
