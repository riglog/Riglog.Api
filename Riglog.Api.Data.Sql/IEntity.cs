using System;
namespace Riglog.Api.Data.Sql
{
    public interface IEntity
    {
        public Guid Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string UpdatedBy { get; set; }
    }
}
