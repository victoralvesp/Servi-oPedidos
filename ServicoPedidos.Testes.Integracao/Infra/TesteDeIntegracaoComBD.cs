using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ServicoPedidos.Dominio;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio.DTOs;
using ServicoPedidos.Dominio.Excecoes;
using ServicoPedidos.Infra.Contextos;
using ServicoPedidos.Infra.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicoPedidos.Testes.Integracao.Infra
{
    [TestFixture]
    public class TesteDeIntegracaoComBD
    {
        IRepositorioDePedidos _repositorio;
        IDiretorDeRequisicoesDePedidos _diretor;


        [OneTimeSetUp]
        public void PreparaBaseDeDadosTeste()
        {
            ConfiguracoesTeste.ConfiguraDataBase();
        }

        [SetUp]
        public void PreparaClassesDeApoio()
        {
            ServiceProvider serviceProvider = ConfiguracoesTeste.ConfiguraServiceProvider();
            _diretor = serviceProvider.GetService<IDiretorDeRequisicoesDePedidos>();
            _repositorio = serviceProvider.GetService<IRepositorioDePedidos>();
        }

        [Test]
        [TestCaseSource(nameof(PedidosParaAdicionar))]
        public void Deve_Adicionar_Pedido(IPedidoDTO pedido)
        {
            //Arrange
            IEnumerable<IPedidoDTO> pedidosDaBaseAntes = null;
            IEnumerable<IPedidoDTO> pedidosDaBaseDepois = null;
            IPedidoDTO pedidoSalvo = null;
            Task<IEnumerable<IPedidoDTO>> taskObterPedidos = _repositorio.ObterPedidosAsync();
            taskObterPedidos.Wait();
            pedidosDaBaseAntes = taskObterPedidos.Result.ToArray();

            //Act
            Task<IPedidoDTO> taskInserirNovoPedido = _diretor.InserirNovoPedidoAsync(pedido);
            taskInserirNovoPedido.Wait();
            pedidoSalvo = taskInserirNovoPedido.Result;


            Task<IEnumerable<IPedidoDTO>> taskObterPedidosDepois = _repositorio.ObterPedidosAsync();
            taskObterPedidosDepois.Wait();
            pedidosDaBaseDepois = taskObterPedidosDepois.Result.ToArray();

            //Assert
            Assert.IsNotNull(pedidoSalvo, "pedido não foi salvo");
            Assert.Greater(pedidosDaBaseDepois.Count(), pedidosDaBaseAntes.Count(), "pedido foi salvo de forma errada");
        }

        [Test]
        [TestCaseSource(nameof(PedidosInvalidos))]
        public void Deve_Lancar_Excecao_Para_Pedido_Invalido(IPedidoDTO pedido)
        {
            //Arrange & Act
            Task<IPedidoDTO> taskInserirNovoPedido = _diretor.InserirNovoPedidoAsync(pedido);

            //Assert
            Assert.Throws(typeof(AggregateException), () => taskInserirNovoPedido.Wait());
        }


        [Test]
        [TestCaseSource(nameof(PedidosParaAlterar))]
        public void Deve_Alterar_Cliente_Do_Pedido(IPedidoDTO pedido)
        {
            //Arrange
            ConfiguracoesTeste.ExecutarScriptsParaTestes().Wait();
            IPedidoDTO pedidoAntes = null;
            using(var contexto = new ContextoPedidos(ConfiguracoesTeste.OptionsConnectionTeste()))
            {
                Task<PedidoModeloBD> taskPedido = contexto.Pedidos.Include(x => x.ItensBD).Where(p => p.Id == pedido.Id).SingleAsync();

                taskPedido.Wait();
                pedidoAntes = taskPedido.Result;
            }

            //Act
            Task<IPedidoDTO> taskPedidoDepois = _diretor.AlterarPedidoAsync(pedido);
            taskPedidoDepois.Wait();
            IPedidoDTO pedidoDepois = taskPedidoDepois.Result;

            //Assert
            Assert.AreNotEqual(pedidoAntes.IdCliente, pedidoDepois.IdCliente);
        }

        [TestCaseSource(nameof(PedidosParaAlterar))]
        public void Deve_Alterar_Quantidade_De_Itens_Do_Pedido(IPedidoDTO pedido)
        {
            //Arrange
            ConfiguracoesTeste.ExecutarScriptsParaTestes().Wait();
            IPedidoDTO pedidoAntes = null;
            using (var contexto = new ContextoPedidos(ConfiguracoesTeste.OptionsConnectionTeste()))
            {
                Task<PedidoModeloBD> taskPedido = contexto.Pedidos.Include(x => x.ItensBD).Where(p => p.Id == pedido.Id).SingleAsync();

                taskPedido.Wait();
                pedidoAntes = taskPedido.Result;
            }

            //Act
            Task<IPedidoDTO> taskPedidoDepois = _diretor.AlterarPedidoAsync(pedido);
            taskPedidoDepois.Wait();
            IPedidoDTO pedidoDepois = taskPedidoDepois.Result;

            //Assert
            Assert.AreNotEqual(pedidoAntes.Itens.Count(), pedidoDepois.Itens.Count());
        }
        
        [TearDown]
        public void DeletarInsercoes()
        {
            ConfiguracoesTeste.DeletarInsercoes().Wait();
        }

        [OneTimeTearDown]
        public void DeletarBase()
        {
            ConfiguracoesTeste.DropDatabaseAsync().Wait();
        }

        public static IEnumerable<object[]> PedidosParaAdicionar()
        {
            IItemDePedidoDTO item1 = new ItemDePedidoDTO() { IdProduto = 1, PrecoUnitario = new ValorMonetario(550001.01M), Quantidade = 1 };
            IItemDePedidoDTO item2 = new ItemDePedidoDTO() { IdProduto = 2, PrecoUnitario = new ValorMonetario(59000.00M), Quantidade = 2 };
            yield return new object[] { new PedidoDTO(new[] { item1, item2 }) { IdCliente = 1 } };
            yield return new object[] { new PedidoDTO(new[] { item1 }) { IdCliente = 2 } };
        }

        public static IEnumerable<object[]> PedidosParaAlterar()
        {
            IItemDePedidoDTO item1 = new ItemDePedidoDTO() { IdProduto = 1, PrecoUnitario = new ValorMonetario(550001.01M), Quantidade = 2 };
            IItemDePedidoDTO item2 = new ItemDePedidoDTO() { IdProduto = 2, PrecoUnitario = new ValorMonetario(59000.00M), Quantidade = 4 };
            IItemDePedidoDTO itemSalvo1 = new ItemDePedidoDTO() { Id = 1, IdProduto = 1, PrecoUnitario = new ValorMonetario(550001.01M), Quantidade = 1 };

            yield return new object[] { new PedidoDTO(new[] { item1 }) { Id = 1, IdCliente = 5 } };
            yield return new object[] { new PedidoDTO(new[] { item2 }) { Id = 2, IdCliente = 5 } };
            yield return new object[] { new PedidoDTO(new[] { item1, item2, itemSalvo1 }) { Id = 1, IdCliente = 5 } };
        }

        public static IEnumerable<object[]> PedidosInvalidos()
        {
            IItemDePedidoDTO itemInvalidoQuantidade = new ItemDePedidoDTO() { IdProduto = 2, PrecoUnitario = new ValorMonetario(59000.00M), Quantidade = 3 };

            IItemDePedidoDTO itemInvalidoRentabilidade = new ItemDePedidoDTO() { IdProduto = 2, PrecoUnitario = new ValorMonetario(1.00M), Quantidade = 2 };
            yield return new object[] { new PedidoDTO(new[] { itemInvalidoQuantidade }) { IdCliente = 5 } };
            yield return new object[] { new PedidoDTO(new[] { itemInvalidoRentabilidade }) { IdCliente = 5 } };
        }

    }
}
