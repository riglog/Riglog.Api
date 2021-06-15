namespace Riglog.Api.Data.Sql.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = default!;

        public string? Password { get; set; }

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; }  = default!;

        public string? Phone { get; set; }

        public bool IsSuperAdmin { get; set; }
    }
}
