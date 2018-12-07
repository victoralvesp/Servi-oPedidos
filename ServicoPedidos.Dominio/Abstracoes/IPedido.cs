using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IPedido : IValidavel, IEntidade
    {
        IEnumerable<IItemDePedido> Itens { get; }
        ICliente Cliente { get; }

    }
}
