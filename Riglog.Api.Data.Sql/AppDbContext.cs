using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql
{
    public class AppDbContext : DbContext
    {
        // User
        public DbSet<User> Users => Set<User>();
        
        // Operating System
        public DbSet<OsType> OsTypes => Set<OsType>();
        public DbSet<OsEdition> OsEditions => Set<OsEdition>();
        public DbSet<OsVersion> OsVersions => Set<OsVersion>();

        // Computer
        public DbSet<Computer> Computers => Set<Computer>();
        public DbSet<ComputerType> ComputerTypes => Set<ComputerType>();
        public DbSet<ComputerUser> ComputerUsers => Set<ComputerUser>();

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
            
            // OsType
            modelBuilder.Entity<OsType>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<OsType>().HasIndex(i => i.IsDeleted);

            // OsEdition
            modelBuilder.Entity<OsEdition>().HasIndex(i => i.IsDeleted);

            // OsVersion
            modelBuilder.Entity<OsVersion>().HasIndex(i => i.IsDeleted);

            // Computer
            modelBuilder.Entity<Computer>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<Computer>().HasIndex(i => i.IsDeleted);

            // ComputerType
            modelBuilder.Entity<ComputerType>().HasIndex(i => i.Name).IsUnique();
            modelBuilder.Entity<ComputerType>().HasIndex(i => i.IsDeleted);

            // ComputerUser
            modelBuilder.Entity<ComputerUser>().HasIndex(i => i.IsAdmin);
            modelBuilder.Entity<ComputerUser>().HasIndex(i => i.IsDeleted);
        }
    }
}
