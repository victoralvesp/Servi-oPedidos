using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Definicoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio
{
    public abstract partial class Rentabilidade : Enumeration<string>, IComparable<Rentabilidade>
    {
        public static Rentabilidade Otima = new RentabilidadeOtima();
        public static Rentabilidade Boa = new RentabilidadeBoa();
        public static Rentabilidade Ruim = new RentabilidadeRuim();

        protected Rentabilidade(string id, string name) : base(id, name)
        {
        }

        public abstract int Score { get; }

        public int CompareTo(Rentabilidade other)
        {
            return Score.CompareTo(other.Score);
        }

        public static bool operator >(Rentabilidade a, Rentabilidade b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator <(Rentabilidade a, Rentabilidade b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator ==(Rentabilidade lhs, Rentabilidade rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Rentabilidade lhs, Rentabilidade rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static bool operator >=(Rentabilidade lhs, Rentabilidade rhs)
        {
            return lhs > rhs || lhs == rhs;
        }

        public static bool operator <=(Rentabilidade lhs, Rentabilidade rhs)
        {
            return lhs < rhs || lhs == rhs;
        }

    }

    internal class RentabilidadeOtima : Rentabilidade
    {
        internal RentabilidadeOtima() : base(Constantes.ID_RENTABILIDADE_OTIMA, Constantes.DESCRICAO_RENTABILIDADE_OTIMA)
        {
        }

        public override int Score => 1;
    }

    internal class RentabilidadeBoa : Rentabilidade
    {
        internal RentabilidadeBoa() : base(Constantes.ID_RENTABILIDADE_BOA, Constantes.DESCRICAO_RENTABILIDADE_BOA)
        {
        }

        public override int Score => 0;
    }

    internal class RentabilidadeRuim : Rentabilidade
    {
        internal RentabilidadeRuim() : base(Constantes.ID_RENTABILIDADE_RUIM, Constantes.DESCRICAO_RENTABILIDADE_RUIM)
        {
        }

        public override int Score => -1;
    }
}
