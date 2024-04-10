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

            builder.Entity<Account>()
            .HasKey(b => b.Id);

            builder.Entity<Log>()
                .ToTable("Logs");

            builder.Entity<Log>()
            .HasKey(b => b.Id);

            builder.Entity<Security>()
                .ToTable("Securities");

            builder.Entity<Security>()
            .HasKey(b => b.Id);

            builder.Entity<User>()
                .ToTable("Users");

            builder.Entity<User>()
            .HasKey(b => b.Id);

            builder.Entity<Transaction>()
                .ToTable("Transactions");

            builder.Entity<Transaction>()
            .HasKey(b => b.Id);

            // Define las foreign keys
            builder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Security>()
                .HasOne(s => s.User)
                .WithMany(u => u.Securities)
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Security> Securities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
