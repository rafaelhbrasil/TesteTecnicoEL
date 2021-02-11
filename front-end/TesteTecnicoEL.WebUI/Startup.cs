using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Linq;
using TesteTecnicoEL.AcessoDados;
using TesteTecnicoEL.Dominio;
using TesteTecnicoEL.Dominio.Usuarios;
using TesteTecnicoEL.WebUI.Filters;

namespace TesteTecnicoEL.WebUI
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
            services.AddControllersWithViews();
            services.AddMvc((options) =>
            {
                options.Filters.Add<SetPropertiesActionFilter>();
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Home/Index";
                    });

            Configuration.Bind("AppSettings", AppSettings.Instance);

            services.AddScoped<Cliente>((sp) =>
            {
                var jsonCliente = sp.GetService<IHttpContextAccessor>().HttpContext?.User?.Claims.FirstOrDefault()?.Value;
                if (string.IsNullOrEmpty(jsonCliente))
                    return null;
                var cliente = JsonConvert.DeserializeObject<Cliente>(jsonCliente);
                return cliente;
            });

            services.AddScoped<IAluguelRepositorio, AluguelRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IVeiculoRepositorio, VeiculoRepositorio>();

            services.AddScoped<IHttpRequest, HttpRequestBase>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton(AppSettings.Instance);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
