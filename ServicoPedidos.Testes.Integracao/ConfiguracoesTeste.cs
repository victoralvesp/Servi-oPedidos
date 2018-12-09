using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServicoPedidos.Testes.Integracao
{
    public static class ConfiguracoesTeste
    {
        private const string ConnectionStringMasterTeste = "Server=ec2-18-231-93-130.sa-east-1.compute.amazonaws.com;Initial Catalog=master;Persist Security Info=False;User ID=sa;Password=sql1Teste2sa;Encrypt=False;TrustServerCertificate=False;";
        private const string ConnectionStringPedidosTeste = "Server=ec2-18-231-93-130.sa-east-1.compute.amazonaws.com;Initial Catalog=PedidosTeste;Persist Security Info=False;User ID=sa;Password=sql1Teste2sa;Encrypt=False;TrustServerCertificate=False;";


        private const string CAMINHO_REPOSITORIO_DATABASE = "../ServicoPedidos-Database";
        private const string CAMINHO_SCRIPTS_TABELAS = "Tables";
        private const string CAMINHO_SCRIPTS_ADICIONAIS = "Scripts";
        private const string CAMINHO_SCRIPTS_TESTES = "Testes";
        private const string COMANDO_BASE_ORIGINAL = "USE Pedidos";
        private const string COMANDO_BASE_TESTE = "USE PedidosTeste";

        public static ServiceProvider ConfiguraServiceProvider()
        {
            StartupTeste startup = new StartupTeste();

            startup.ConfigureServices();

            var serviceProvider = startup.Services.BuildServiceProvider();
            return serviceProvider;
        }

        public static void ConfiguraDataBase()
        {
            DbContext dbContext = ConfiguraDbContext();
            CriaBaseETabelasAsync(dbContext).Wait();
            ExecutaScriptsAdicionais(dbContext).Wait();
        }

        public static async Task DropDatabaseAsync()
        {
            DbContext context = ConfiguraDbContext();
            await context.Database.ExecuteSqlCommandAsync(SCRIPT_DROP_DATABASE);
        }

        private static DbContext ConfiguraDbContext()
        {
            var opcoesDeConexaoComStringTeste = new DbContextOptionsBuilder().UseSqlServer(ConnectionStringMasterTeste).Options;

            var dbContext = new DbContext(opcoesDeConexaoComStringTeste);
            return dbContext;
        }

        private static async Task CriaBaseETabelasAsync(DbContext context)
        {
            await context.Database.ExecuteSqlCommandAsync(CriaScriptDatabase());

            foreach (string script in CriaScripts(CAMINHO_SCRIPTS_TABELAS, 'T'))
            {
                await context.Database.ExecuteSqlCommandAsync(script);
            }


        }

        private static string CriaScriptDatabase()
        {
            string script = @"CREATE DATABASE [PedidosTeste]";

            return script;
        }

        private static async Task ExecutaScriptsAdicionais(DbContext context)
        {
            foreach (string script in CriaScripts(CAMINHO_SCRIPTS_ADICIONAIS, 'S'))
            {
                await context.Database.ExecuteSqlCommandAsync(script);
            }
        }

        internal static DbContextOptions OptionsConnectionTeste()
        {
            return new DbContextOptionsBuilder().UseSqlServer(ConnectionStringPedidosTeste).Options;
        }

        public static async Task ExecutarScriptsParaTestes()
        {
            DbContext context = ConfiguraDbContext();
            foreach (string script in CriaScripts(CAMINHO_SCRIPTS_TESTES, 'X'))
            {
                await context.Database.ExecuteSqlCommandAsync(script);
            }
        }

        private static IEnumerable<string> CriaScripts(string caminhoScripts, char letraInicialScripts)
        {
            string caminhoDoDiretorioDeScriptsDeTabelas = DiretorioDoRepositorioDeBaseDeDados(caminhoScripts);

            string[] caminhoDosScriptsDeTestes = Directory.GetFiles(caminhoDoDiretorioDeScriptsDeTabelas);
            caminhoDosScriptsDeTestes = OrdenaScripts(caminhoDosScriptsDeTestes, letraInicialScripts);

            foreach (var caminhoScript in caminhoDosScriptsDeTestes)
            {
                string script = File.ReadAllText(caminhoScript).SubstituiComandosParaTeste();

                script += "\r\n";

                yield return script;
            }
        }

        

        private static string[] OrdenaScripts(string[] caminhoDosScripts, char letraInicialDosScripts = 'T')
        {
            caminhoDosScripts = caminhoDosScripts.OrderBy((caminho) =>
            {
                string nome = Path.GetFileName(caminho);
                string Pattern = letraInicialDosScripts + @"([0-9]*)\w";
                Match match = Regex.Match(nome, Pattern);
                return match.Success ? int.Parse(match.Groups[1].Value) : 0;
            }).ToArray();
            return caminhoDosScripts;
        }

        internal static async Task DeletarInsercoes()
        {
            DbContext context = ConfiguraDbContext();
            await context.Database.ExecuteSqlCommandAsync(ScriptDeleteItensDePedidos());
            await context.Database.ExecuteSqlCommandAsync(ScriptDeletePedidos());
        }

        private static string ScriptDeletePedidos()
        {
            return "USE PedidosTeste " +
                   "DELETE FROM Pedidos";
        }

        private static string ScriptDeleteItensDePedidos()
        {
            return "USE PedidosTeste " +
                   "DELETE FROM Itens_de_Pedido";
        }

        private static string SubstituiComandosParaTeste(this string script)
        {
            return script.Replace(COMANDO_BASE_ORIGINAL, COMANDO_BASE_TESTE)
                         .Replace("\nGO", "")
                         .Replace("\n GO", "");
        }

        private static string DiretorioDoRepositorioDeBaseDeDados(string caminhoScripts)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            while (Path.GetDirectoryName(baseDirectory).Contains("Pedidos"))
            {
                baseDirectory = Path.GetDirectoryName(baseDirectory);
            }

            string caminhoDoDiretorioDeScriptsDeTabelas = Path.Combine(baseDirectory, CAMINHO_REPOSITORIO_DATABASE, caminhoScripts);
            return caminhoDoDiretorioDeScriptsDeTabelas;
        }


        public const string SCRIPT_DROP_DATABASE = @"USE [master];

                                                    DECLARE @kill varchar(8000) = '';  
                                                    SELECT @kill = @kill + 'kill ' + CONVERT(varchar(5), session_id) + ';'  
                                                    FROM sys.dm_exec_sessions
                                                    WHERE database_id  = db_id('PedidosTeste')

                                                    EXEC(@kill);

                                                    IF EXISTS (
                                                      SELECT [name]
                                                       FROM sys.databases
                                                       WHERE [name] = N'PedidosTeste'
                                                    )
                                                    DROP DATABASE PedidosTeste";

    }
}
