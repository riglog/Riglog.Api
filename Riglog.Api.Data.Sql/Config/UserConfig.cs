using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(k => new { k.Username }).IsUnique();
            builder.HasIndex(k => new { k.Email }).IsUnique();
            builder.HasIndex(k => new { k.Phone }).IsUnique();
            builder.HasIndex(i => i.IsSuperAdmin);
            builder.HasIndex(i => i.IsDeleted);
        }
    }
}