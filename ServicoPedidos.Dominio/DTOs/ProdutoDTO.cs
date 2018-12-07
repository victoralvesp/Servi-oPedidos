using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.DTOs
{
    public class ProdutoDTO : IProdutoDTO
    {
        public int Id { get; set; }

        public int Multiplo { get; set; }
        public string Nome { get; set; }
        public ValorMonetario PrecoSugerido { get; set; }
    }
}
