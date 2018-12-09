using Newtonsoft.Json;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicoPedidos.WebAPI.JsonConverters
{
    public class JsonConverterDeCliente : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(IClienteDTO).IsAssignableFrom(objectType) || typeof(ClienteDTO).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var Cliente = new ClienteDTO();

            serializer.Populate(reader, Cliente);

            return Cliente;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
