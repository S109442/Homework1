using Microsoft.EntityFrameworkCore;
using Homework1.Models;
using System.Collections.Generic;

namespace Homework1.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
