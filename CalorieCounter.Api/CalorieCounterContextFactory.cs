using System.IO;
using CalorieCounter.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CalorieCounter.Api
{
    public class CalorieCounterContextFactory : IDesignTimeDbContextFactory<CalorieCounterContext>
    {
        public CalorieCounterContext CreateDbContext(string[] args)
        {
             IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

             var builder = new DbContextOptionsBuilder<CalorieCounterContext>();
             var connectionString = configuration.GetConnectionString("CalorieCounterDb");
             builder.UseSqlServer(connectionString, b => b.MigrationsAssembly("CalorieCounter.Api"));
             return new CalorieCounterContext(builder.Options);    
        }
    }
}