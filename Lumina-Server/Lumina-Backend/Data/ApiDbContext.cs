using Lumina_Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Lumina_Backend.Data
{
    public class ApiDbContext(DbContextOptions<ApiDbContext> options) : DbContext(options)
    {
        public override int SaveChanges()
        {
            // Actualiza la fecha de modificación para los modelados que referencian el BaseEntity, solo actualiza los modelos que use cada API
            var entities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is BaseEntity)
                .Select(e => e.Entity as BaseEntity);

            foreach (var entity in entities)
            {
                if (entity != null)
                {
                    entity.DateUpdated = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }

        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
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
                .IsRequired();

            builder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber)
                .IsRequired();

            builder.Entity<Security>()
                .HasOne(s => s.User)
                .WithMany(u => u.Securities)
                .IsRequired();

            builder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs)
                .IsRequired();

            var demoUsers = new List<User>
            {
                new() { Id = -1, UserName = "ajruiz2204", Password = HashPassword("123456"), Email = "ajruiz2204@example.com", FullName = "Alejandro Ruíz"},
                new() { Id = -2, UserName = "AlexanderFRT", Password = HashPassword("123456"), Email = "alexanderfrt@example.com", FullName = "Alexander Flores"},
                new() { Id = -3, UserName = "ezealeguzman", Password = HashPassword("123456"), Email = "ezealeguzman@example.com", FullName = "Ezequiel Guzman"},
                new() { Id = -4, UserName = "facu597", Password = HashPassword("123456"), Email = "facu597@example.com", FullName = "Facundo Castro"},
                new() { Id = -5, UserName = "karla6524", Password = HashPassword("123456"), Email = "karla6524@example.com", FullName = "Karla Chavez"},
                new() { Id = -6, UserName = "mabel8750_", Password = HashPassword("123456"), Email = "mabel8750_@example.com", FullName = "Mabel Ceballos"}
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
