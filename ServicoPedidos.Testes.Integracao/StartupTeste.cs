using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Dominio;
using ServicoPedidos.Infra.Repositorios;
using Microsoft.AspNetCore.Hosting;
using ServicoPedidos.Infra.Contextos;
using Microsoft.EntityFrameworkCore;
using ServicoPedidos.WebAPI.JsonConverters;

namespace ServicoPedidos.Testes.Integracao
{
    public class StartupTeste
    {
        public StartupTeste()
        {
            Configuration = CarregarArquivoDeConfiguracoes();
        }

        public IConfiguration Configuration { get; }
        public ServiceCollection Services { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices()
        {
            Services = new ServiceCollection();
            Services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new JsonContractResolverPadrao();
                    });

            Services.AddDbContext<ContextoPedidos>(options => options.UseSqlServer(Configuration.GetConnectionString("ConexaoPedidos")));

            Services.AddScoped<IDiretorDeRequisicoesDePedidos, DiretorDeRequisicoesDePedidos>();
            Services.AddScoped<IConversorDeDTOs, ConversorDeDTOs>();
            Services.AddScoped<IRepositorioDePedidos, RepositorioDePedidos>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }



        public IConfigurationRoot CarregarArquivoDeConfiguracoes()
        {
            var basePath = Path.GetDirectoryName(
                               Path.GetDirectoryName(
                                   Path.GetDirectoryName(TestContext.CurrentContext.TestDirectory)));


            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettingsteste.json")
                .Build();

            return config;
        }
    }
}
