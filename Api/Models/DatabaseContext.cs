using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Data> Data { get; set; }
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("user");

            modelBuilder.Entity<Data>()
                .ToTable("data");
        }

    }
}
