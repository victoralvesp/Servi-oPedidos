using NUnit.Framework;
using ServicoPedidos.Dominio;
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
            Assert.Throws(typeof(RentabilidadeInvalidaException), () => pedido.Validar());
        }

        private static IEnumerable<object[]> ItensValidos()
        {
            Produto produto = new Produto("Produto teste", 1, new ValorMonetario(100));
            yield return new object[] { new ItemDePedido(produto, new ValorMonetario(90), 10) };
            yield return new object[] { new ItemDePedido(produto, new ValorMonetario(110), 10) };
        }

        private static IEnumerable<object[]> ItensQuantidadeInvalida()
        {
            Produto produtoMultiplo2 = new Produto("Produto teste", 2, new ValorMonetario(100));
            Produto produtoMultiplo3 = new Produto("Produto teste", 3, new ValorMonetario(100));
            yield return new object[] { new ItemDePedido(produtoMultiplo2, new ValorMonetario(90), 9) };
            yield return new object[] { new ItemDePedido(produtoMultiplo2, new ValorMonetario(110), 11) };
            yield return new object[] { new ItemDePedido(produtoMultiplo3, new ValorMonetario(110), 1) };
            yield return new object[] { new ItemDePedido(produtoMultiplo3, new ValorMonetario(90), 0) };
        }

        private static IEnumerable<object[]> PedidosSemItens()
        {
            Produto produto = new Produto("Produto teste", 1, new ValorMonetario(100));
            Cliente cliente = new Cliente("Cliente teste");

            yield return new object[] { new Pedido(cliente, new IItemDePedido[] { }) };
        }

        private static IEnumerable<object[]> PedidosRentabilidadeRuim()
        {
            Produto produto = new Produto("Produto teste", 1, new ValorMonetario(100));
            Cliente cliente = new Cliente("Cliente teste");
            ItemDePedido itemRentabilidadeRuim = new ItemDePedido(produto, new ValorMonetario(89.99M), 10);
            ItemDePedido itemRentabilidadeBoa = new ItemDePedido(produto, new ValorMonetario(90), 10);
            ItemDePedido itemRentabilidadeOtima = new ItemDePedido(produto, new ValorMonetario(100.1M), 10);
            List<ItemDePedido> itensDePedidos = new List<ItemDePedido>();
            itensDePedidos.Add(itemRentabilidadeRuim);

            IItemDePedido[] itensComUmItem = itensDePedidos.ToArray();
            itensDePedidos.Add(itemRentabilidadeBoa);
            IItemDePedido[] itensComDoisItens = itensDePedidos.ToArray();
            itensDePedidos.Add(itemRentabilidadeOtima);
            IItemDePedido[] itensComTresItens = itensDePedidos.ToArray();

            yield return new object[] { new Pedido(cliente, itensComUmItem) };
            yield return new object[] { new Pedido(cliente, itensComDoisItens) };
            yield return new object[] { new Pedido(cliente, itensComTresItens) };
        }

        private static IEnumerable<object[]> PedidosValidos()
        {
            Produto produto = new Produto("Produto teste", 1, new ValorMonetario(100));
            Cliente cliente = new Cliente("Cliente teste");
            ItemDePedido itemRentabilidadeBoa = new ItemDePedido(produto, new ValorMonetario(90), 10);
            ItemDePedido itemRentabilidadeOtima = new ItemDePedido(produto, new ValorMonetario(100.1M), 10);
            List<ItemDePedido> itensDePedidos = new List<ItemDePedido>();
            itensDePedidos.Add(itemRentabilidadeBoa);

            IItemDePedido[] itensComUmItem = itensDePedidos.ToArray();
            itensDePedidos.Add(itemRentabilidadeOtima);
            IItemDePedido[] itensComDoisItens = itensDePedidos.ToArray();

            yield return new object[] { new Pedido(cliente, itensComUmItem) };
            yield return new object[] { new Pedido(cliente, itensComDoisItens) };
        }
    }
}