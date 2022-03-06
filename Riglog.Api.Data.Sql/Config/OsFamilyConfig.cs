using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config;

public class OsFamilyConfig : IEntityTypeConfiguration<OsFamily>
{
    public void Configure(EntityTypeBuilder<OsFamily> builder)
    {
        builder.HasIndex(i => i.Name).IsUnique();
        builder.HasIndex(i => i.IsDeleted);
    }
}