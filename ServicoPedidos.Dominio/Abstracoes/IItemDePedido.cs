using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IItemDePedido : IValidavel
    {
        IProduto Produto { get; }
        Rentabilidade Rentabilidade { get; }
        ValorMonetario PrecoUnitario { get; }
    }
}
