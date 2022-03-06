using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql;

public class AppDbContext : DbContext
{
    // User
    public DbSet<User> Users => Set<User>();
        
    // Operating System
    public DbSet<OsFamily> OsFamilies => Set<OsFamily>();
    public DbSet<OsDistribution> OsDistributions => Set<OsDistribution>();
    public DbSet<OsVersion> OsVersions => Set<OsVersion>();
    public DbSet<OsEdition> OsEditions => Set<OsEdition>();

    // Computer
    public DbSet<Computer> Computers => Set<Computer>();
    public DbSet<ComputerType> ComputerTypes => Set<ComputerType>();
    public DbSet<ComputerUser> ComputerUsers => Set<ComputerUser>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}