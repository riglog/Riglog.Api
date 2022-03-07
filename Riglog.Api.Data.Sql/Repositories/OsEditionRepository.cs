using Riglog.Api.Data.Sql.Entities;
using Riglog.Api.Data.Sql.Interfaces;

namespace Riglog.Api.Data.Sql.Repositories;

public class OsEditionRepository : GenericRepository<OsEdition>, IOsEditionRepository
{
    public OsEditionRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
}