using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities
{
    public class OsVersion : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Computer> Computers { get; set; }

        public ICollection<OsEdition> OsEdition { get; set; }

    }
}