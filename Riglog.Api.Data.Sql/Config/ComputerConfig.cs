using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config;

public class ComputerConfig : IEntityTypeConfiguration<Computer>
{
    public void Configure(EntityTypeBuilder<Computer> builder)
    {
        builder
            .HasOne(e => e.OsDistribution)
            .WithMany(e => e.Computers)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasIndex(i => i.Name).IsUnique();
        builder.HasIndex(i => i.IsDeleted);
    }
}