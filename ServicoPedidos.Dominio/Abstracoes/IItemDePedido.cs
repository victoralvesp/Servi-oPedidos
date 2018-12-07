using ServicoPedidos.Dominio.Rentabilidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IItemDePedido : IValidavel, IEntidade
    {
        IProduto Produto { get; }
        Rentabilidade Rentabilidade { get; }
        int Quantidade { get; }
        ValorMonetario PrecoUnitario { get; }
    }
}
