using ContaCerta.Domain.Common.Interfaces;
using ContaCerta.Domain.Costs.Model;

namespace ContaCerta.Domain.Costs.Repositories.Interfaces;

public interface ICostRepository : IRepository<Cost>
{
    public IEnumerable<Cost> LastCostsUserId(int UserId, int LastDays);
    public IEnumerable<Cost> NextCostsUserId(int UserId);
}
