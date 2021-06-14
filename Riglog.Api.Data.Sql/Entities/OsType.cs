using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities
{
    public class OsType : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Computer> Computers { get; set; }

        public OsEdition OsEdition { get; set; }

    }
}