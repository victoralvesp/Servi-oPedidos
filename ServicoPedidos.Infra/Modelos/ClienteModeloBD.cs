using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Infra.Modelos
{
    public class ClienteModeloBD : IClienteDTO
    {
        public string Nome { get; set; }
        public int Id { get; set; }
    }
}
