using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Infrastructure.Data.ApplicationContext;
using DTO.Db;

namespace Infrastructure.Services
{
    public class DbService : IDbService
    {
        private readonly IConfigurationRoot config;
        private readonly ILogger<IDbService> logger;
        private readonly ApplicationDbContext context;

        public DbService(ILoggerFactory loggerFactory, IConfigurationRoot configurationRoot, ApplicationDbContext context)
        {
            this.logger = loggerFactory.CreateLogger<DbService>();
            this.config = configurationRoot;
            this.context = context;
        }

        public async Task SaveStrategy(StrategyMemory strategyMemory)
        {
            await context.StrategyMemory.AddAsync(strategyMemory);
            await context.SaveChangesAsync();
        }

        public async Task SaveListStrategy(List<StrategyMemory> listStrategyMemory)
        {
            await context.StrategyMemory.AddRangeAsync(listStrategyMemory);
            await context.SaveChangesAsync();
        }
    }
}