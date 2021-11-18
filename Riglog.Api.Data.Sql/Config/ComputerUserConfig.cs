using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config;

public class ComputerUserConfig : IEntityTypeConfiguration<ComputerUser>
{
    public void Configure(EntityTypeBuilder<ComputerUser> builder)
    {
        builder.HasIndex(i => i.IsAdmin);
        builder.HasIndex(i => i.IsDeleted);
    }
}