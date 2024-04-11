using Lumina_Backend.Models;
using Microsoft.EntityFrameworkCore;

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

            builder.Entity<User>()
                .ToTable("Users");

            builder.Entity<Account>()
                .ToTable("Accounts");

            builder.Entity<Transaction>()
                .ToTable("Transactions");

            builder.Entity<Log>()
                .ToTable("Logs");

            builder.Entity<Security>()
                .ToTable("Securities");

            // Define las relaciones
            builder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserID)
                .IsRequired();

            builder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountID)
                .IsRequired();

            builder.Entity<Security>()
                .HasOne(s => s.User)
                .WithMany(u => u.Securities)
                .HasForeignKey(s => s.UserId)
                .IsRequired();

            builder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.UserId)
                .IsRequired();

            var demoUsers = new List<User>
            {
            new User { Id = -1, UserName = "ajruiz2204", Password = "123456", Email = "ajruiz2204@example.com"},
            new User { Id = -2, UserName = "4rnol", Password = "123456", Email = "4rnol@example.com"},
            new User { Id = -3, UserName = "AlexanderFRT", Password = "123456", Email = "alexanderfrt@example.com"},
            new User { Id = -4, UserName = "ezealeguzman", Password = "123456", Email = "ezealeguzman@example.com"},
            new User { Id = -5, UserName = "giolucc", Password = "123456", Email = "giolucc@example.com"}
            };
            builder.Entity<User>().HasData(demoUsers);
         }

        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<Log> Logs { get; set; }

    }
}
