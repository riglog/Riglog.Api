using System;
using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities;

public class OsDistribution : BaseEntity
{
    public string Name { get; set; } = default!;

    public Guid OsFamilyId { get; set; } = default!;
    public OsFamily OsFamily { get; set; } = default!;
    
    public ICollection<Computer>? Computers { get; set; }

    public ICollection<OsEdition>? OsEditions { get; set; }
    
    public ICollection<OsVersion>? OsVersions { get; set; }
}