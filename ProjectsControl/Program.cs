using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectsControl.Models;

namespace ProjectsControl
{
    public class Program
    {
        public static void Main(string[] args)
        {                        
            var host = CreateHostBuilder(args).Build();
            var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DBProjectContext>();
                context.Database.EnsureCreated();
            }catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("An error occurred creating the database");
            }
            host.Run();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
