using System.Threading.Tasks;
using Riglog.Api.Data.Sql.Entities;

namespace Riglog.Api.Data.Sql.Interfaces;

public interface IComputerTypeRepository : IGenericRepository<ComputerType>
{
    public Task<ComputerType> GetByNameAsync(string name);
}