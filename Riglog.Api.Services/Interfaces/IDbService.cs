using System.Collections.Generic;
using System.Threading.Tasks;

namespace Riglog.Api.Services.Interfaces;

public interface IDbService
{
    public Task<IEnumerable<string>>GetMigrationsAsync();

    public Task MigrateAsync();
}