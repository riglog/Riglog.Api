using System;
namespace Riglog.Api.Data.Sql
{
    public class BaseEntity : IEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; } = DateTime.Now;

        public string UpdatedBy { get; set; }
    }
}
