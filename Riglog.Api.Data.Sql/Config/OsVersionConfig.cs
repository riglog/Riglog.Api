using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config
{
    public class OsVersionConfig : IEntityTypeConfiguration<OsVersion>
    {
        public void Configure(EntityTypeBuilder<OsVersion> builder)
        {
            builder.HasIndex(i => i.IsDeleted);
        }
    }
}