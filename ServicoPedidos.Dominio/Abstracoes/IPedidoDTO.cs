using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{ 
    public interface IPedidoDTO : IEntidade
    {
        IEnumerable<IItemDePedidoDTO> Itens { get; set; }
        int IdCliente { get; set; }
    }
}
