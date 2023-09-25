using Microsoft.EntityFrameworkCore;
using DTO.Db;

namespace Infrastructure.Data.ApplicationContext
{
    /// <summary>
    /// Entity framework context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        { 

        }
        public DbSet<StrategyMemory> StrategyMemory { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

    #region Sqllite
    //public static class SqliteConsoleContextFactory
    //{
    //    public static SqliteConsoleContext Create(string connectionString)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<SqliteConsoleContext>();
    //        optionsBuilder.UseSqlServer(connectionString);

    //        var context = new SqliteConsoleContext(optionsBuilder.Options);
    //        context.Database.EnsureCreated();

    //        return context;
    //    }
    //}
    #endregion
}