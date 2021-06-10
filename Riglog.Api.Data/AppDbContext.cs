using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Entities;

namespace Riglog.Api.Data
{
    public class AppDbContext : DbContext
    {
        // User
        public DbSet<User> Users => Set<User>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>().HasIndex(k => new { k.Username }).IsUnique();
            modelBuilder.Entity<User>().HasIndex(k => new { k.Email }).IsUnique();
            modelBuilder.Entity<User>().HasIndex(k => new { k.Phone }).IsUnique();
            modelBuilder.Entity<User>().HasIndex(i => i.IsSuperAdmin);
            modelBuilder.Entity<User>().HasIndex(i => i.IsDeleted);
        }
    }
}
