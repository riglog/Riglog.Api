using System;
namespace Riglog.Api.Data.Sql;

public class BaseEntity : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public bool IsDeleted { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now.ToUniversalTime();

    public Guid CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; } = DateTime.Now.ToUniversalTime();

    public Guid UpdatedBy { get; set; }
}