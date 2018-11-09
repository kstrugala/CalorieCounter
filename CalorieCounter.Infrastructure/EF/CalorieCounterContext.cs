using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CalorieCounter.Infrastructure.Options;

namespace CalorieCounter.Infrastructure.EF
{
    public class CalorieCounterContext : DbContext
    {
        private readonly IOptions<SqlOptions> _settings;

        public CalorieCounterContext(DbContextOptions<CalorieCounterContext> options, IOptions<SqlOptions> settings):base(options)
        {
            _settings = settings;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_settings.Value.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

    }
}