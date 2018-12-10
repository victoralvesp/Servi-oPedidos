using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicoPedidos.Dominio.Abstracoes;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ServicoPedidos.WebAPI.JsonConverters
{
    public class JsonConverterDeValorMonetario : JsonConverter
    {
        private const char Separator = ' ';
        private const string numberOfDecimalPlaces = "N6";
        private const string PropriedadeValor = nameof(ValorMonetario.Valor);
        private const string PropriedadeMoeda = nameof(ValorMonetario.Moeda);

        public override bool CanWrite => false;
        public override bool CanRead => true;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ValorMonetario);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jsonObject = JObject.Load(reader);
            string ValorMonetarioBase = jsonObject.GetValue(PropriedadeValor, StringComparison.OrdinalIgnoreCase).Value<String>();


            string[] pedacosDoValorMonetario = ValorMonetarioBase.Split(Separator);
            decimal valor;
            string idMoeda;
            Moeda moeda;
            if (decimal.TryParse(ValorMonetarioBase, style: NumberStyles.Currency, provider: CultureInfo.CurrentCulture, result: out valor))
            {
                JToken jToken = jsonObject.GetValue(PropriedadeMoeda, StringComparison.OrdinalIgnoreCase);

                if (!jToken.HasValues)
                {
                    idMoeda = jToken.Value<String>();
                    moeda = Moeda.Obtem(idMoeda);
                }
                else
                {
                    moeda = serializer.Deserialize<Moeda>(jToken.CreateReader());
                }
            }
            else
            {
                valor = decimal.Parse(pedacosDoValorMonetario[0], style: NumberStyles.Currency, provider: CultureInfo.CurrentCulture);

                idMoeda = pedacosDoValorMonetario[1];
                moeda = Moeda.Obtem(idMoeda);
            }

            ValorMonetario resultado = new ValorMonetario(valor, moeda);

            return resultado;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null)
            {
                serializer.Serialize(writer, null);
                return;
            }

            writer.WriteStartObject();

            if (value is ValorMonetario ValorMonetario)
            {
                string valorSerializado = ValorMonetario.ToString();
                writer.WritePropertyName(PropriedadeValor);
                writer.WriteValue(valorSerializado);
            }
            else
            {
                throw new ArgumentException(String.Format("Uso incorreto do conversor de ValorMonetario. {0} não é do tipo ValorMonetario", value.GetType().Name));
            }

            writer.WriteEndObject();

        }

    }
}
