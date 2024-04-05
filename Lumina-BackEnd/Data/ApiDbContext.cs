﻿using Lumina_BackEnd.Models;
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
            builder.Entity<Account>()
                .HasOne(a => a.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(a => a.UserID);

            builder.Entity<Security>()
                .HasOne(s => s.User)
                .WithMany(u => u.Securities)
                .HasForeignKey(s => s.UserId);

            builder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs)
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
