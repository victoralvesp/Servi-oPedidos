using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IProduto
    {
        int Multiplo { get; }
        ValorMonetario PrecoSugerido { get; }
    }
}
