using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.DTOs
{
    public class PedidoDTO : IPedidoDTO
    {
        public PedidoDTO(IEnumerable<IItemDePedidoDTO> itens = null)
        {
            Itens = itens;
            if(itens == null)
            {
                Itens = new List<IItemDePedidoDTO>();
            }
        }

        public IEnumerable<IItemDePedidoDTO> Itens { get; set; }
        public int IdCliente { get; set; }
        public int Id { get; set; }
    }
}
