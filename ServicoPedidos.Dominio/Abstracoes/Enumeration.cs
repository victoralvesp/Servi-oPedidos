using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ServicoPedidos.Dominio.Abstracoes
{
    public abstract class Enumeration<T> : IComparable where T : IComparable
    {
        public virtual string Name { get; private set; }
        public virtual T Id { get; private set; }

        protected Enumeration()
        {
        }

        protected Enumeration(T id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        public static IEnumerable<U> GetAll<U>() where U : Enumeration<T>
        {
            var type = typeof(U);
            var fields = type.GetTypeInfo().GetFields(BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly
                );
            foreach (var info in fields)
            {
                if (info.GetValue(null) is U locatedValue)
                {
                    yield return locatedValue;
                }
            }
        }

        public static U Parse<U>(string name) where U : Enumeration<T>
        {
            return GetAll<U>().First(tipo => string.Equals(tipo.Name, name, StringComparison.CurrentCultureIgnoreCase));
        }

        public static U Get<U>(T id) where U : Enumeration<T>
        {
            return GetAll<U>().First(tipo => tipo.Id.Equals(id));
        }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((Enumeration<T>)other).Id);
        }

        public override bool Equals(object obj)
        {
            if (obj?.GetType() != this?.GetType())
            {
                return false;
            }

            var otherValue = obj as Enumeration<T>;

            var valueMatches = Id.Equals(otherValue.Id);
            return valueMatches;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Enumeration<T> a, Enumeration<T> b)
        {
            object objA = a;
            object objB = b;

            if (objA == null)
                return objB == null;

            return a.Equals(b);
        }

        public static bool operator !=(Enumeration<T> a, Enumeration<T> b)
        {
            object objA = a;
            object objB = b;

            if (objA == null)
                return objB != null;

            return !(a.Equals(b));
        }
    }

    public class Enumeration : Enumeration<int>
    {
        protected Enumeration()
        {
        }

        protected Enumeration(int id, string name) : base(id, name)
        {
        }
    }
}
