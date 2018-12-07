using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.DTOs
{
    public class ClienteDTO : IClienteDTO
    {
        public string Nome { get; set; }
        public int Id { get; set; }
    }
}
