using LanchesBufaNew.Context;
using LanchesBufaNew.Models;
using LanchesBufaNew.Repositories;
using LanchesBufaNew.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LanchesBufaNew;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<ILancheRepository, LancheRepository>();
        services.AddScoped(sp => CarrinhoCompra.GetCarrinho(sp));

        services.AddControllersWithViews();
        services.AddMemoryCache();
        services.AddSession();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            //Faz um mapeamento padrão de rota
            //endpoints.MapDefaultControllerRoute();

            endpoints.MapControllerRoute(
                name: "categoriaFiltro",
                pattern: "Lanche/{action}/{categoria?}",
                defaults: new { controller = "Lanche", Action = "List" });
            
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}