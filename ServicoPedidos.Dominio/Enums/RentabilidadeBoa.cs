using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Definicoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Rentabilidades
{
    internal class RentabilidadeBoa : Rentabilidade
    {
        internal RentabilidadeBoa() : base(Constantes.ID_RENTABILIDADE_BOA, Constantes.DESCRICAO_RENTABILIDADE_BOA)
        {
        }

        public override int Score => 0;
    }

    public abstract partial class Rentabilidade : Enumeration<string>, IComparable<Rentabilidade>
    {
        public static Rentabilidade Boa = new RentabilidadeBoa();
    }
}
