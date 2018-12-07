using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Definicoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Rentabilidades
{
    internal class RentabilidadeRuim : Rentabilidade
    {
        internal RentabilidadeRuim() : base(Constantes.ID_RENTABILIDADE_RUIM, Constantes.DESCRICAO_RENTABILIDADE_RUIM)
        {
        }

        public override int Score => -1;
    }

    public abstract partial class Rentabilidade : Enumeration<string>, IComparable<Rentabilidade>
    {
        public static Rentabilidade Ruim = new RentabilidadeRuim();
    }
}
