using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Infra.Modelos
{
    public class ProdutoModeloBD : IProdutoDTO
    {
        public string Nome { get; set; }
        public int? Multiplo { get; set; }
        public int Id { get; set; }
        public ValorMonetario PrecoSugerido { get; set; }

        public string PrecoSugeridoMoeda
        {
            get
            {
                if (PrecoSugerido.Moeda != null)
                {
                    return PrecoSugerido.Moeda.Id;
                }
                else
                {
                    return Moeda.Real.Id;
                }
            }

            set
            {
                if (value != null)
                {
                    PrecoSugerido = new ValorMonetario(PrecoSugerido.Valor, Moeda.Obtem(value));
                }
                else
                {
                    PrecoSugerido = new ValorMonetario(PrecoSugerido.Valor);
                }
            }
        }

        public decimal PrecoSugeridoValor
        {
            get => PrecoSugerido.Valor;
            set => PrecoSugerido = new ValorMonetario(value, Moeda.Obtem(PrecoSugeridoMoeda));
        }

    }
}
