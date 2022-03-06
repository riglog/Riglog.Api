using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities;

public class Computer : BaseEntity
{
    public string Name { get; set; } = default!;

    public ComputerType ComputerType { get; set; } = default!;
    
    public OsFamily OsFamily { get; set; } = default!;
    
    public OsDistribution OsDistribution { get; set; } = default!;
    
    public OsVersion? OsVersion { get; set; }
    
    public OsEdition? OsEdition { get; set; }

    public ICollection<ComputerUser>? ComputerUsers { get; set; }
        
}