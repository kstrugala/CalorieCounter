using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using CalorieCounter.Core.Domain;

namespace CalorieCounter.Infrastructure.EF
{
    public class CalorieCounterContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Product> Products { get; set; }

        public CalorieCounterContext(DbContextOptions<CalorieCounterContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                var userBuilder = modelBuilder.Entity<User>();
                userBuilder.HasKey(x=>x.Id);

                var refreshTokenBuilder = modelBuilder.Entity<RefreshToken>();
                refreshTokenBuilder.HasKey(x=>x.Email);

                var productsBuilder = modelBuilder.Entity<Product>();;
                productsBuilder.HasKey(x=>x.Id);

        }

    }
}