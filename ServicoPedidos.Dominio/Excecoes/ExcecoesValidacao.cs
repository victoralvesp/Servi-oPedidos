using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Dominio.Excecoes
{

    [Serializable]
    public class QuantidadeInvalidaException : Exception
    {
        public QuantidadeInvalidaException() { }
        public QuantidadeInvalidaException(string message) : base(message) { }
        public QuantidadeInvalidaException(string message, Exception inner) : base(message, inner) { }
        protected QuantidadeInvalidaException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class RentabilidadeInvalidaException : Exception
    {
        public RentabilidadeInvalidaException() { }
        public RentabilidadeInvalidaException(string message) : base(message) { }
        public RentabilidadeInvalidaException(string message, Exception inner) : base(message, inner) { }
        protected RentabilidadeInvalidaException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class PedidoInvalidoException : Exception
    {
        public PedidoInvalidoException() { }
        public PedidoInvalidoException(string message) : base(message) { }
        public PedidoInvalidoException(string message, Exception inner) : base(message, inner) { }
        protected PedidoInvalidoException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
