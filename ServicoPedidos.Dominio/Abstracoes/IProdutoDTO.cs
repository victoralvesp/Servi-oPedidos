using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IProdutoDTO : IEntidade
    {
        string Nome { get; set; }
        int Multiplo { get; set; }
        ValorMonetario PrecoSugerido { get; set; }
    }
}
