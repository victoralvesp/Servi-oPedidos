using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServicoPedidos.Infra.Modelos
{
    public class PedidoModeloBD : IPedidoDTO
    {
        internal PedidoModeloBD()
        {
        }

        public PedidoModeloBD(IPedido pedido)
        {
            Id = pedido.Id;
            ItensBD = pedido.Itens.Select(item => new ItemDePedidoModeloBD(item));
            IdCliente = pedido.Cliente.Id;
        }

        public PedidoModeloBD(IPedidoDTO pedido)
        {
            Id = pedido.Id;
            ItensBD = pedido.Itens.Select(item => new ItemDePedidoModeloBD(item)).ToList();
            IdCliente = pedido.IdCliente;
        }

        public IEnumerable<IItemDePedidoDTO> Itens { get => ItensBD; set => value.Select(item => new ItemDePedidoModeloBD(item) { IdPedido = this.Id }); }
        public int IdCliente { get; set; }
        public int Id { get; set; }

        public IEnumerable<ItemDePedidoModeloBD> ItensBD { get; set; }
    }
}