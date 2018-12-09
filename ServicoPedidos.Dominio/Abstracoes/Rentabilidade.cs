using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Definicoes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Rentabilidades
{
    public abstract partial class Rentabilidade : Enumeration<string>, IComparable<Rentabilidade>
    {
        public abstract int Score { get; }
        protected Rentabilidade(string id, string name) : base(id, name)
        {
        }

        public int CompareTo(Rentabilidade other)
        {
            return Score.CompareTo(other.Score);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator >(Rentabilidade a, Rentabilidade b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator <(Rentabilidade a, Rentabilidade b)
        {
            return a.CompareTo(b) < 0;
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


}
