using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceCenterWeb.Models;
using ServiceCenterWeb.Utils;

namespace ServiceCenterWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //Default: CreateWebHostBuilder(args).Build().Run();

            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try {
                    var userManager = services?.GetRequiredService<UserManager<User>>();
                    var rolesManager = services?.GetRequiredService<RoleManager<IdentityRole>>();
                    await RoleInitializer.InitializeAsync(userManager, rolesManager);
                } catch (Exception ex) {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger?.LogError(ex, "Ошибка при начальном заполнении БД");
                } // try-catch
            } // using

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
