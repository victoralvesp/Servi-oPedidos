using NUnit.Framework;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Definicoes;
using ServicoPedidos.Dominio.Excecoes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServicoPedidos.Testes.Unitarios
{
    [TestFixture]
    public class ValidacoesDePedidoTeste
    {
        [Test]
        [TestCaseSource(nameof(ItensValidos))]
        public void Itens_De_Pedidos_Validos_Nao_Devem_Lancar_Excecao(IItemDePedido item)
        {
            Assert.DoesNotThrow(() => item.Validar());
        }

        [Test]
        [TestCaseSource(nameof(PedidosValidos))]
        public void Pedidos_Com_Itens_Validos_Nao_Devem_Lancar_Excecao(IPedido pedido)
        {
            Assert.DoesNotThrow(() => pedido.Validar());
        }


        [Test]
        [TestCaseSource(nameof(ItensQuantidadeInvalida))]
        public void Itens_Com_Quantidades_Invalidas_Devem_Lancar_Excecao_Especifica(IItemDePedido item)
        {
            Assert.Throws(typeof(QuantidadeInvalidaException), () => item.Validar(), Mensagens.EXCECAO_QUANTIDADE_INVALIDA(item.Produto.Multiplo.ToString())); 
        }

        [Test]
        [TestCaseSource(nameof(PedidosRentabilidadeRuim))]
        public void Pedidos_Com_Itens_Com_Rentabilidade_Ruim_Devem_Lancar_Excecao(IPedido pedido)
        {
            Assert.Throws(typeof(RentabilidadeInvalidaException), () => pedido.Validar(), Mensagens.EXCECAO_QUANTIDADE_INVALIDA());
        }

        private static IEnumerable<object[]> ItensValidos()
        {
            yield return null;
        }

        private static IEnumerable<object[]> ItensQuantidadeInvalida()
        {
            yield return null;
        }

        private static IEnumerable<object[]> PedidosSemItens()
        {
            yield return null;
        }

        private static IEnumerable<object[]> PedidosRentabilidadeRuim()
        {
            yield return null;
        }

        private static IEnumerable<object[]> PedidosValidos()
        {
            yield return null;
        }
    }
}