using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public struct ValorMonetario : IComparable<ValorMonetario>
    {
        private const char SEPARATOR = ' ';
        private Moeda _moeda;

        public Moeda Moeda { get => _moeda ?? Moeda.Real; private set => _moeda = value; }
        public decimal Valor { get; private set; }

        public static ValorMonetario Zero = default(ValorMonetario);

        public ValorMonetario(decimal valor = 0, Moeda moeda = null)
        {
            _moeda = moeda;

            Valor = valor;
        }

        public static ValorMonetario EmReais(decimal valor)
        {
            return new ValorMonetario(valor: valor);
        }

        public int CompareTo(ValorMonetario other)
        {
            if (!VerificaCompatibilidade(this, other))
            {
                throw new InvalidOperationException(String.Format("As moedas {0} e {1} sao incompativeis para realizar a comparação", other.Moeda.Name, Moeda.Name));
            }

            return Valor.CompareTo(other.Valor);

        }

        public int CompareTo(object obj)
        {
            if (!(obj is ValorMonetario other))
                throw new ArgumentException(String.Format("Não é possível comparar objeto do tipo {0} com Dinheiro", obj.GetType().Name));

            return CompareTo(other);
        }


        public static ValorMonetario operator +(ValorMonetario a, ValorMonetario b)
        {
            if (!VerificaCompatibilidade(a, b))
                throw new InvalidOperationException("Não é possível adicionar moedas diferente. Converta o dinheiro para a mesma moeda antes de adicionar");

            decimal valorResultante = a.Valor + b.Valor;

            ValorMonetario resultado;
            if (a.Valor != 0)
                resultado = new ValorMonetario(valorResultante, a.Moeda);
            else
                resultado = new ValorMonetario(valorResultante, b.Moeda);

            return resultado;
        }

        public static ValorMonetario operator -(ValorMonetario a, ValorMonetario b)
        {
            if (!VerificaCompatibilidade(a, b))
                throw new InvalidOperationException("Não é possível subtrair moedas diferente. Converta o dinheiro para a mesma moeda antes de subtrair");

            decimal valorResultante = a.Valor - b.Valor;

            ValorMonetario resultado;
            if (a.Valor != 0)
                resultado = new ValorMonetario(valorResultante, a.Moeda);
            else
                resultado = new ValorMonetario(valorResultante, b.Moeda);

            return resultado;
        }

        private static bool VerificaCompatibilidade(ValorMonetario a, ValorMonetario b)
        {
            return a.Moeda == b.Moeda || (a.Valor == 0) || (b.Valor == 0);
        }


        public static ValorMonetario MenorEntre(ValorMonetario a, ValorMonetario b)
        {
            int comparacao = a.CompareTo(b);

            if (comparacao > 0)
                return b;
            else
                return a;
        }


        public static ValorMonetario MaiorEntre(ValorMonetario a, ValorMonetario b)
        {
            int comparacao = a.CompareTo(b);

            if (comparacao < 0)
                return b;
            else
                return a;
        }

        public override bool Equals(object obj)
        {
            if (obj is ValorMonetario other)
            {
                if (other.Valor == 0 || Valor == 0)
                {
                    return Valor.Equals(other.Valor);
                }
                else
                {
                    return Valor.Equals(other.Valor) && Moeda.Equals(other.Moeda);
                }
            }
            else
            {
                return false;
            }

        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;

                if (Valor != 0)
                {
                    hash = hash * 7 + Moeda.GetHashCode();
                }

                hash = hash * 7 + Valor.GetHashCode();

                return hash;
            }
        }

        public string ToStringSimplificada()
        {
            return Valor.ToString() + SEPARATOR + Moeda.Id;
        }


        public static ValorMonetario operator *(ValorMonetario a, decimal k)
        {
            ValorMonetario resultado = new ValorMonetario(k * a.Valor, a.Moeda);

            return resultado;
        }

        public static ValorMonetario operator *(decimal k, ValorMonetario a)
        {
            ValorMonetario resultado = new ValorMonetario(k * a.Valor, a.Moeda);

            return resultado;
        }

        public static ValorMonetario operator *(ValorMonetario a, double k)
        {
            return a * (decimal)k;
        }

        public static ValorMonetario operator *(ValorMonetario a, float k)
        {
            return a * (decimal)k;
        }

        public static ValorMonetario operator *(double k, ValorMonetario a)
        {
            return a * (decimal)k;
        }

        public static ValorMonetario operator *(float k, ValorMonetario a)
        {
            return a * (decimal)k;
        }

        public static ValorMonetario operator /(ValorMonetario a, decimal k)
        {
            if (k == 0.0M)
                throw new ArgumentException("Não se pode dividir um dinheiro por 0");

            ValorMonetario resultado = new ValorMonetario(a.Valor / k, a.Moeda);

            return resultado;
        }

        public static ValorMonetario operator /(ValorMonetario a, double k)
        {
            return a / (decimal)k;
        }

        public static ValorMonetario operator /(ValorMonetario a, float k)
        {
            return a / (decimal)k;
        }

        public static decimal operator /(ValorMonetario a, ValorMonetario b)
        {
            if (!VerificaCompatibilidade(a, b))
                throw new InvalidOperationException("Não se pode dividir valores em dinheiro com moedas distintas");

            if (b.Valor == 0.0M)
            {
                throw new ArgumentException("Não se pode dividir um dinheiro por 0");
            }

            return a.Valor / b.Valor;
        }


        public static bool operator >(ValorMonetario a, ValorMonetario b)
        {
            return a.CompareTo(b) > 0;
        }

        public static bool operator <(ValorMonetario a, ValorMonetario b)
        {
            return a.CompareTo(b) < 0;
        }

        public static bool operator ==(ValorMonetario lhs, ValorMonetario rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(ValorMonetario lhs, ValorMonetario rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static bool operator >=(ValorMonetario lhs, ValorMonetario rhs)
        {
            return lhs > rhs || lhs == rhs;
        }

        public static bool operator <=(ValorMonetario lhs, ValorMonetario rhs)
        {
            return lhs < rhs || lhs == rhs;
        }

        public static ValorMonetario Arredondar(ValorMonetario valor, int decimais = 8)
        {
            return new ValorMonetario(decimal.Round(valor.Valor, decimais), valor.Moeda);
        }
    }
}
