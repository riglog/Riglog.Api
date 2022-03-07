using System.Threading.Tasks;

namespace Riglog.Api.Services.Interfaces;

public interface ISeedService
{
    public Task SeedAdminUserAsync(string adminPassword);

    public Task SeedOs();
}