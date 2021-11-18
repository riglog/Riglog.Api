using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config;

public class OsTypeConfig : IEntityTypeConfiguration<OsType>
{
    public void Configure(EntityTypeBuilder<OsType> builder)
    {
        builder.HasIndex(i => i.Name).IsUnique();
        builder.HasIndex(i => i.IsDeleted);
    }
}