using System.Threading.Tasks;

namespace Riglog.Api.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Login(string username, string password);
    }
}