using Newtonsoft.Json.Serialization;
using ServicoPedidos.Dominio;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServicoPedidos.WebAPI.JsonConverters
{
    public class JsonContractResolverPadrao : DefaultContractResolver
    {
        protected override JsonContract CreateContract(Type objectType)
        {
            JsonContract contract = base.CreateContract(objectType);

            bool conversorEncontrado = false;

            if (!conversorEncontrado && (objectType == typeof(IItemDePedidoDTO) || objectType == typeof(ItemDePedidoDTO)))
            {
                conversorEncontrado = true;
                contract.Converter = new JsonConverterDeItemDePedido();
            }
            if (!conversorEncontrado && (objectType == typeof(IPedidoDTO) || objectType == typeof(PedidoDTO)))
            {
                conversorEncontrado = true;
                contract.Converter = new JsonConverterDePedido();
            }
            if (!conversorEncontrado && (objectType == typeof(IProdutoDTO) || objectType == typeof(ProdutoDTO)))
            {
                conversorEncontrado = true;
                contract.Converter = new JsonConverterDeProduto();
            }
            if (!conversorEncontrado && (objectType == typeof(IClienteDTO) || objectType == typeof(ClienteDTO)))
            {
                conversorEncontrado = true;
                contract.Converter = new JsonConverterDeCliente();
            }






            if (!conversorEncontrado && objectType == typeof(ValorMonetario))
            {
                conversorEncontrado = true;
                contract.Converter = new JsonConverterDeValorMonetario();
            }



            if (!conversorEncontrado && (typeof(Enumeration<string>).IsAssignableFrom(objectType) || typeof(Enumeration<int>).IsAssignableFrom(objectType)))
            {
                conversorEncontrado = true;
                contract.Converter = new JsonConverterDeEnumeration();
            }

            return contract;
        }
    }
}
