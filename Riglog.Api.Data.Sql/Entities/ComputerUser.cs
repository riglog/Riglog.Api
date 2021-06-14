namespace Riglog.Api.Data.Sql.Entities
{
    public class ComputerUser : BaseEntity
    {
        public User User { get; set; }

        public Computer Computer { get; set; }

        public bool IsAdmin { get; set; }
    }
}