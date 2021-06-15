using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Config
{
    public class OsEditionConfig : IEntityTypeConfiguration<OsEdition>
    {
        public void Configure(EntityTypeBuilder<OsEdition> builder)
        {
            builder.HasIndex(i => i.IsDeleted);
        }
    }
}