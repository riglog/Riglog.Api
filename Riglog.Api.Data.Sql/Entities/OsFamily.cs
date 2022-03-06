using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities;

public class OsFamily : BaseEntity
{
    public string Name { get; set; } = default!;
    
    public ICollection<Computer>? Computers { get; set; }
    
    public ICollection<OsDistribution>? OsDistributions { get; set; }
}