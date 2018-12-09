using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public class Cliente : ICliente
    {
        public Cliente(string nome)
        {
            Nome = nome;
        }

        public int Id { get; set; }

        public string Nome { get; }
    }
}
