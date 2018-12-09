using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public class Produto : IProduto
    {
        public Produto(string nome, int multiplo, ValorMonetario precoSugerido)
        {
            Nome = nome;
            Multiplo = multiplo;
            PrecoSugerido = precoSugerido;
        }

        public string Nome { get; }

        public int Multiplo { get; }

        public ValorMonetario PrecoSugerido { get; }

        public int Id { get; set; }
    }
}
