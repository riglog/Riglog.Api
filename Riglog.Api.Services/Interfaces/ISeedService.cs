using System.Threading.Tasks;

namespace Riglog.Api.Services.Interfaces
{
    public interface ISeedService
    {
        public Task SeedAdminUser(string adminPassword);
    }
}