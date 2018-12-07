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
            yield return null;
        }
    }
}
