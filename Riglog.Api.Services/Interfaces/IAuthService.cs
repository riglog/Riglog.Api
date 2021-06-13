namespace Riglog.Api.Services.Interfaces
{
    public interface IAuthService
    {
        public string Login(string username, string password);
    }
}