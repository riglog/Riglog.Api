using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities;

public class ComputerType : BaseEntity
{
    public string Name { get; set; } = default!;

    public ICollection<Computer>? Computers { get; set; }

}