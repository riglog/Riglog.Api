using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config;

public class OsTypeConfig : IEntityTypeConfiguration<OsDistribution>
{
    public void Configure(EntityTypeBuilder<OsDistribution> builder)
    {
        builder.HasIndex(i => new { i.Name, i.OsFamilyId }).IsUnique();
        builder.HasIndex(i => i.IsDeleted);
    }
}