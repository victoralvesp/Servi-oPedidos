using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public class Produto : IProduto
    {
        public Produto(string nome, ValorMonetario precoSugerido, int? multiplo)
        {
            Nome = nome;
            if(multiplo == null || multiplo == 0)
            {
                Multiplo = 1;
            }
            else
            {
                Multiplo = multiplo.Value;
            }

            PrecoSugerido = precoSugerido;
        }

        public string Nome { get; }

        public int Multiplo { get; }

        public ValorMonetario PrecoSugerido { get; }

        public int Id { get; set; }
    }
}
