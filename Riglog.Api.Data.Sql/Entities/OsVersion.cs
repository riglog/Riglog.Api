using System;
using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities;

public class OsVersion : BaseEntity
{
    public string Name { get; set; } = default!;
    
    public Guid OsDistributionId { get; set; } = default!;
    public OsDistribution OsDistribution { get; set; } = default!;

    public ICollection<Computer>? Computers { get; set; }
    
}