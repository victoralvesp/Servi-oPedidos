using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public sealed class Moeda : Enumeration<string>
    {
        public string Unidade { get; private set; }

        public static readonly Moeda Real = new Moeda("BRL", "Real", "R$");

        private static readonly Func<IEnumerable<Moeda>> fabricaDeMoedas = () => GetAll<Moeda>();
        private static Lazy<IEnumerable<Moeda>> _todasAsMoedas = new Lazy<IEnumerable<Moeda>>(fabricaDeMoedas);

        internal static IEnumerable<Moeda> TodasAsMoedas { get => _todasAsMoedas.Value; }

        public static Moeda Obtem(string id) => Get<Moeda>(id);

        private Moeda(string id, string name, string unidade) : base(id.ToUpperInvariant(), name)
        {
            Unidade = unidade;
        }

        private Moeda()
        {
        }

    }
}
