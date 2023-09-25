using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Infrastructure.Services.BinanceService;
using Infrastructure.Data.ApplicationContext;
using Infrastructure.Services;

namespace MainProgram.ServicesComponent
{
    internal class InitServices
    {
        private ServiceProvider services { get; }

        internal protected InitServices()
        {
            // Configuration
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var configuration = builder.Build();

            // Database
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            var context = new ApplicationDbContext(optionsBuilder.Options);
            //context.Database.EnsureCreated(); //uncomment when connecting with db....


            // Services
            this.services = new ServiceCollection()
                .AddLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConsole();
                })
                .AddSingleton(configuration)
                .AddSingleton(optionsBuilder.Options)
                .AddSingleton<IDbService, DbService>()
                .AddSingleton<IBinanceService>(provider =>
                {
                    return new BinanceService(provider.GetRequiredService<ILoggerFactory>());
                })
                .AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")))
                .BuildServiceProvider();


            //IF DEBUG MODE
            var logger = (services.GetService<ILoggerFactory>() ?? throw new InvalidOperationException())
                .CreateLogger<Program>();

            logger.LogInformation($"InitServices application at: {DateTime.Now}");

        }

        internal protected ServiceProvider GetServices()
        {
            return this.services;
        }

    }


}
