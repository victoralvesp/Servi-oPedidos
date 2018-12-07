using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IClienteDTO : IEntidade
    {
        int Id { get; }
    }
}
