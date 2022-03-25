using BackEndApiEF.data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEndApiEF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var appHost = CreateHostBuilder(args).Build();
            SeedDataIfNotExist(appHost);
                
               appHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    
    private static void SeedDataIfNotExist(IHost host)
        {
            using(var seedingScope = host.Services.CreateScope())
            {
                var servicesProvider = seedingScope.ServiceProvider;
                try 
                {
                    var projectDbContext = servicesProvider.GetRequiredService<ProjectDbContext>();
                    SeedData.seed(projectDbContext);
                } 
                catch(Exception ex) 
                {
                    var programLogger = servicesProvider.GetRequiredService<ILogger<Program>>();
                    programLogger.LogError(ex, "Fatal Error occured creating Database");
                }
            }
        }
    
    }


}
