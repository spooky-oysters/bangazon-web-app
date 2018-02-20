using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Bangazon.Data;
using Microsoft.AspNetCore.Identity;
using Bangazon.Models;

namespace Bangazon
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            /*
                Author: Krys Mathis
                Summary: Initiates the process to seed the database. The method
                        uses the service provider to send a user manager
                        to the DbInitializer.Initalize static method.
                        Errors are logged and sent to the console.
            */
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetService<UserManager<ApplicationUser>>();
                    DbInitializer.InitializeAsync(services,  userManager);
                    
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
