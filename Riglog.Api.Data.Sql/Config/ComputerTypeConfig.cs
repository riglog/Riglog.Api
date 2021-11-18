using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config;

public class ComputerTypeConfig : IEntityTypeConfiguration<ComputerType>
{
    public void Configure(EntityTypeBuilder<ComputerType> builder)
    {
        builder.HasIndex(i => i.Name).IsUnique();
        builder.HasIndex(i => i.IsDeleted);
    }
}