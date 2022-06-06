using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Identity.Context;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Service.Identity
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            var host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ApplicationDbContext>();
                await context.Database.MigrateAsync();

                var key = config.GetValue<string>("PasswordKey");
                await Seed.SeedData(context, key);
            }
            catch (Exception e)
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), Environment.NewLine + Environment.NewLine + DateTime.Now.ToString() + " => " + e.Message +":"+e.InnerException);
                return;
            }

            try
            {
                host.Run();
            }
            catch (Exception e)
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), Environment.NewLine + Environment.NewLine + DateTime.Now.ToString() + " => " + e.Message);
                return;
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
