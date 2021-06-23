using System.Threading.Tasks;

namespace Riglog.Api.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> LoginAsync(string username, string password);
    }
}