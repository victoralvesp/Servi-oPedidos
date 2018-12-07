using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IProduto : IEntidade
    {
        string Nome { get; }
        int Multiplo { get; }
        ValorMonetario PrecoSugerido { get; }
    }
}
