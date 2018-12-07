using ServicoPedidos.Dominio.Rentabilidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public interface IAnalisadorDeRentabilidade
    {
        Rentabilidade CalcularRentabilidade(ValorMonetario precoSugerido, ValorMonetario precoUnitario);
    }
}
