namespace Riglog.Api.Data.Sql.Entities
{
    public class ComputerUser : BaseEntity
    {
        public User User { get; set; } = default!;

        public Computer Computer { get; set; } = default!;
        public bool IsAdmin { get; set; }
    }
}