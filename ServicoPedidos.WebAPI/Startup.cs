using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Formatters.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServicoPedidos.Dominio;
using ServicoPedidos.Dominio.Abstracoes;
using ServicoPedidos.Infra.Contextos;
using ServicoPedidos.Infra.Repositorios;
using ServicoPedidos.WebAPI.JsonConverters;

namespace ServicoPedidos.WebAPI
{
    public class Startup
    {
        private const string OPEN_POLICY = "OpenPolicy";
        private const string BR_CULTURE = "pt-BR";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(opt => opt.AddPolicy(OPEN_POLICY, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                })
            );
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new JsonContractResolverPadrao();
                    });

            services.AddDbContext<ContextoPedidos>(options => options.UseSqlServer(Configuration.GetConnectionString("ConexaoPedidos")));

            services.AddScoped<IDiretorDeRequisicoesDePedidos, DiretorDeRequisicoesDePedidos>();
            services.AddScoped<IConversorDeDTOs, ConversorDeDTOs>();
            services.AddScoped<IRepositorioDePedidos, RepositorioDePedidos>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(OPEN_POLICY);
            var cultureInfo = new CultureInfo(BR_CULTURE);


            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(cultureInfo),
                SupportedCultures = new List<CultureInfo>
                                        {
                                            cultureInfo,
                                        },
                SupportedUICultures = new List<CultureInfo>
                                          {
                                              cultureInfo,
                                          }
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
