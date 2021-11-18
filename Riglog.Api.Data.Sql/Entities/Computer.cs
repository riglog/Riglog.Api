using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities;

public class Computer : BaseEntity
{
    public string Name { get; set; } = default!;

    public ComputerType ComputerType { get; set; } = default!;

    public OsType OsType { get; set; } = default!;

    public OsEdition? OsEdition { get; set; }

    public OsVersion? OsVersion { get; set; }

    public ICollection<ComputerUser>? ComputerUsers { get; set; }
        
}