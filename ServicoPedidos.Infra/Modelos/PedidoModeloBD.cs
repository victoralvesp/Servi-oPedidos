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
        public IEnumerable<IItemDePedidoDTO> Itens { get => ItensBD; set => value.Select(item => new ItemDePedidoModeloBD(item) { IdPedido = this.Id }); }
        public int IdCliente { get; set; }
        public int Id { get; set; }

        public IEnumerable<ItemDePedidoModeloBD> ItensBD { get; set; }
    }
}