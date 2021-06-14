using System.Collections.Generic;

namespace Riglog.Api.Data.Sql.Entities
{
    public class Computer : BaseEntity
    {
        public string Name { get; set; }

        public ComputerType ComputerType { get; set; }

        public OsType OsType { get; set; }

        public OsEdition OsEdition { get; set; }

        public OsVersion OsVersion { get; set; }

        public ICollection<ComputerUser> ComputerUsers { get; set; }
        
    }
}