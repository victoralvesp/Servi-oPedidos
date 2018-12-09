using NUnit.Framework;
using ServicoPedidos.Dominio;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.Rentabilidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServicoPedidos.Testes.Unitarios.Unitarios
{
    [TestFixture]
    public class RentabilidadeTeste
    {
        IAnalisadorDeRentabilidade _analisadorDeRentabilidade;

        [SetUp]
        public void Setup()
        {
            ConstruirObjetoPrincipal();
        }

        [Test]
        [TestCaseSource(nameof(TestCasesRentabilidade))]
        public void Rentabilidade_Deve_Estar_De_Acordo_Com_O_Esperado(IItemDePedido item, Rentabilidade resultado)
        {

            //Arrange & Act
            Rentabilidade rentabilidadeDoItem = _analisadorDeRentabilidade.CalcularRentabilidade(item.Produto.PrecoSugerido, item.PrecoUnitario);

            //Assert
            Assert.AreEqual(resultado, rentabilidadeDoItem);
        }

        private void ConstruirObjetoPrincipal()
        {
            _analisadorDeRentabilidade = new AnalisadorDeRentabilidade();
        }

        private static IEnumerable<object[]> TestCasesRentabilidade()
        {
            Produto produto = new Produto("Produto teste", new ValorMonetario(100), 1);
            yield return new object[] { new ItemDePedido(produto, new ValorMonetario(90), 10), Rentabilidade.Boa };
            yield return new object[] { new ItemDePedido(produto, new ValorMonetario(100.1M), 10), Rentabilidade.Otima };
            yield return new object[] { new ItemDePedido(produto, new ValorMonetario(89.99M), 10), Rentabilidade.Ruim };
            yield return new object[] { new ItemDePedido(produto, new ValorMonetario(100.00M), 10), Rentabilidade.Boa };
            yield return new object[] { new ItemDePedido(produto, new ValorMonetario(110), 10), Rentabilidade.Otima };
        }
    }
}
