using Lumina_BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumina_BackEnd.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>()
                .ToTable("Account");        
            
            builder.Entity<Log>()
                .ToTable("Log");
            
            builder.Entity<Security>()
                .ToTable("Security");

            builder.Entity<User>()
                .ToTable("User"); 
            
            builder.Entity<Transaction>()
                .ToTable("Transaction");
        }

        public DbSet<Account>? Account { get; set; }
        public DbSet<Log>? Logs { get; set; }
        public DbSet<Security>? Security { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<Transaction>? Transaction { get; set; }

    }
}
