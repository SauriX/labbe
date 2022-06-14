using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.MedicalRecord.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Service.MedicalRecord
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
             /*   var key = config.GetValue<string>("PasswordKey");
                await Seed.SeedData(context, key);*/
            }
            catch (Exception e)
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), Environment.NewLine + Environment.NewLine + DateTime.Now.ToString() + " => " + e.Message + ":" + e.InnerException);
                return;
            }

            try
            {
                host.Run();
            }
            catch (Exception e)
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), "log.txt"), Environment.NewLine + Environment.NewLine + DateTime.Now.ToString() + " => " + e.Message + ":" + e.InnerException);
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
