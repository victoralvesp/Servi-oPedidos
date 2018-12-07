using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IItemDePedidoDTO : IEntidade
    {
        int IdProduto { get; set; }
        int Quantidade { get; set; }
        ValorMonetario PrecoUnitario { get; set; }
    }
}
