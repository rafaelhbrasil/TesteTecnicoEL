using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using TesteTecnicoEL.Dominio.Locacao.Repositorios;
using TesteTecnicoEL.Dominio.Locacao.Servicos;
using TesteTecnicoEL.Dominio.Usuarios.Repositorios;
using TesteTecnicoEL.Dominio.Usuarios.Servicos;
using TesteTecnicoEL.Dominio.Veiculos.Repositorios;
using TesteTecnicoEL.Infraestrutura.Memoria.Locacao;
using TesteTecnicoEL.Infraestrutura.Memoria.Usuarios;
using TesteTecnicoEL.Infraestrutura.Memoria.Veiculos;

namespace TesteTecncicoEL.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            InicializarDependencias(services);

            services.AddControllers();
            //services.AddMvc((options) =>
            //{
            //    options.Filters.Add<AuthRequestFilter>();
            //    options.Filters.Add<ResponseHandlerFilter>();
            //});

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API do Rafael",
                    Contact = new OpenApiContact
                    {
                        Name = "Rafael Brasil",
                        Email = "rafaelhbrasil@gmail.com",
                    },
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

        }


        private void InicializarDependencias(IServiceCollection services)
        {
            //Configuration.Bind("AppSettings", AppSettings.Instance);
            //services.AddSingleton(AppSettings.Instance);

            //var connection = Configuration.GetConnectionString(nameof(DatabaseContext));
            //services.AddDbContext<DatabaseContext>(options => options.UseMySql(connection));
            //services.AddScoped<DbContext, DatabaseContext>();

            //services.AddScoped<User>((sp) => {
            //    var user = new User();
            //    sp.GetService<IHttpContextAccessor>().HttpContext?.User.AddIdentity(new UserIdentity(user));
            //    return user;
            //});
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IClienteRepositorio, ClienteRepositorio>();
            services.AddScoped<IOperadorRepositorio, OperadorRepositorio>();
            services.AddScoped<IMarcaRepositorio, MarcaRepositorio>();
            services.AddScoped<IModeloRepositorio, ModeloRepositorio>();
            services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();
            services.AddScoped<IAluguelRepositorio, AluguelRepositorio>();

            services.AddScoped<ServicoAutenticacao>();
            services.AddScoped<ServicoAluguel>();

            // seed inicial para adicionar algo no repositório em memória
            services.AddScoped<SeedInicial>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API do Rafael V1");

            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            InicializarDados(app);
        }

        private void InicializarDados(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                var seed = serviceScope.ServiceProvider.GetService<SeedInicial>();
                seed.Inicializar();
            }
        }
    }
}
