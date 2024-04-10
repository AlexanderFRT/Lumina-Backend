using Lumina_Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Lumina_Backend.Data
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
                .ToTable("Accounts");

            builder.Entity<Log>()
                .ToTable("Logs");

            builder.Entity<Security>()
                .ToTable("Securities");

            builder.Entity<User>()
                .ToTable("Users");

            builder.Entity<Transaction>()
                .ToTable("Transactions");

            // Define las foreign keys
            builder.Entity<User>()
                .HasMany(u => u.Accounts)
                .WithOne(a => a.User)
                .HasForeignKey(a => a.UserID);

            builder.Entity<User>()
                .HasMany(u => u.Securities)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            builder.Entity<User>()
                .HasMany(u => u.Logs)
                .WithOne(l => l.User)
                .HasForeignKey(l => l.UserId); 

            builder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountID);
        }   

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
