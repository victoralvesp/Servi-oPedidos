using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServicoPedidos.Dominio;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.DTOs;
using ServicoPedidos.WebAPI.JsonConverters;

namespace ServicoPedidos.WebAPI.Controllers
{
    [Route("pedidos")]
    public class PedidosController : Controller
    {
        IDiretorDeRequisicoesDePedidos _diretorDeRequisicoes;

        public PedidosController(IDiretorDeRequisicoesDePedidos diretorDeRequisicoes)
        {
            this._diretorDeRequisicoes = diretorDeRequisicoes;
        }

        //GET pedidos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            IPedidoDTO pedido;

            try
            {
                pedido = await _diretorDeRequisicoes.ObterPedidoAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return base.Ok(pedido);
        }

        // POST pedidos
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]IPedidoDTO pedido)
        {
            IPedidoDTO pedidoSalvo;

            try
            {
                //IPedidoDTO pedido = ConverteJsonParaDTO(pedidoJson["pedido"].ToString());
                pedidoSalvo = await _diretorDeRequisicoes.InserirNovoPedidoAsync(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return base.Ok(pedidoSalvo);
        }

        // Até o momento {id} é desnecessário, porém o parâmetro irá permanecer por necessidades futuras
        // PUT pedidos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]IPedidoDTO pedido)
        {
            IPedidoDTO pedidoSalvo;

            try
            {
                //IPedidoDTO pedido = ConverteJsonParaDTO(pedidoJson["pedido"].ToString());
                pedidoSalvo = await _diretorDeRequisicoes.AlterarPedidoAsync(pedido);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return base.Ok(pedidoSalvo);
        }
        
        //private IPedidoDTO ConverteJsonParaDTO(string pedidoJson)
        //{
        //    JsonSerializerSettings settings = new JsonSerializerSettings() { ContractResolver = new JsonContractResolverPadrao() };
        //    return JsonConvert.DeserializeObject<PedidoDTO>(pedidoJson, settings);
        //}
    }
}
