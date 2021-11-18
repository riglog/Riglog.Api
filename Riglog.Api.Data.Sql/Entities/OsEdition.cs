using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities;

public class OsEdition : BaseEntity
{
    public string Name { get; set; } = default!;

    public ICollection<Computer>? Computers { get; set; }

    public ICollection<OsType> OsTypes { get; set; } = default!;

    public OsVersion? OsVersion { get; set; }

}