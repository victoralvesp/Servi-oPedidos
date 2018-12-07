using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Rentabilidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public class AnalisadorDeRentabilidade : IAnalisadorDeRentabilidade
    {
        public Rentabilidade CalcularRentabilidade(ValorMonetario precoSugerido, ValorMonetario precoUnitario)
        {
            if (precoUnitario > precoSugerido)
            {
                return Rentabilidade.Otima;
            }
            else
            {
                ValorMonetario diferencaNoPreco = precoSugerido - precoUnitario;

                decimal escalaDaDiferencaEmPercentual = diferencaNoPreco / precoSugerido * 100;

                if(escalaDaDiferencaEmPercentual >= 10)
                {
                    return Rentabilidade.Boa;
                }
                else
                {
                    return Rentabilidade.Ruim;
                }
            }
        }
    }
}
