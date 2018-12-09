using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ServicoPedidos.WebAPI.JsonConverters
{
    public class JsonConverterDeEnumeration : JsonConverter
    {
        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return typeof(Enumeration<string>).IsAssignableFrom(objectType) || typeof(Enumeration<int>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JToken.Load(reader);
            JToken jobEnumId = jsonObject;

            JEnumerable<JToken> jEnumerable = jsonObject.Children();
            if (!jEnumerable.Equals(JEnumerable<JToken>.Empty) && jsonObject["id"] != null)
            {
                jobEnumId = jsonObject["id"];
            }

            Type enumGenType = typeof(Enumeration<>);

            Type enumTypeParam = null;
            Type enumBase = objectType;
            int counter = 0;
            while (enumTypeParam == null && counter < 100)
            {
                counter++;
                if (enumBase.IsGenericType && enumBase.GetGenericTypeDefinition() == typeof(Enumeration<>))
                {
                    enumTypeParam = enumBase.GetGenericArguments()[0];
                }
                else
                {
                    enumBase = enumBase.BaseType;
                }
            }

            if (enumTypeParam == null)
                throw new InvalidOperationException("The type does not inherit from Enumeration<>");


            object idValue = GetValueFromJtoken(jobEnumId, enumTypeParam);
            Type enumType = enumGenType.MakeGenericType(enumTypeParam);

            MethodInfo getMethodInfo = enumType.GetMethod(nameof(Enumeration.Get), BindingFlags.Static | BindingFlags.Public);

            MethodInfo getMethodGenericInfo = getMethodInfo.MakeGenericMethod(objectType);

            object enumValue = getMethodGenericInfo.Invoke(null, new[] { idValue });

            return enumValue;
        }

        private object GetValueFromJtoken(JToken jobEnumId, Type enumTypeParam)
        {
            Type jtokenType = jobEnumId.GetType();

            var methods = jtokenType.GetMethods();

            MethodInfo getValueMethod = jtokenType.GetMethod(nameof(jobEnumId.ToObject), new Type[] { }).MakeGenericMethod(enumTypeParam);

            return getValueMethod.Invoke(jobEnumId, null);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
