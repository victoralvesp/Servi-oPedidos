using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.DTOs
{
    public class ItemDePedidoDTO : IItemDePedidoDTO
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
        public ValorMonetario PrecoUnitario { get; set; }
        public int Id { get; set; }
    }
}
