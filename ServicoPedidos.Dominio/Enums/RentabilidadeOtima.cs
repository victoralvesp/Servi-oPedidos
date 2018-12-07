using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Definicoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Rentabilidades
{
    internal class RentabilidadeOtima : Rentabilidade
    {
        internal RentabilidadeOtima() : base(Constantes.ID_RENTABILIDADE_OTIMA, Constantes.DESCRICAO_RENTABILIDADE_OTIMA)
        {
        }

        public override int Score => 1;
    }

    public abstract partial class Rentabilidade : Enumeration<string>, IComparable<Rentabilidade>
    {
        public static Rentabilidade Otima = new RentabilidadeOtima();
    }


}
