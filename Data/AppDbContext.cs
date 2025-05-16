using Microsoft.EntityFrameworkCore;
using Homework1.Models;
using System.Collections.Generic;

namespace Homework1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<AccountBook> AccountBook { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountBook>()
                .Property(b => b.Category)
                .HasColumnName("Categoryyy");

            modelBuilder.Entity<AccountBook>()
                .Property(b => b.Amount)
                .HasColumnName("Amounttt");

            modelBuilder.Entity<AccountBook>()
                .Property(b => b.Date)
                .HasColumnName("Dateee");

            modelBuilder.Entity<AccountBook>()
                .Property(b => b.Remark)
                .HasColumnName("Remarkkk");
        }
    }
}
