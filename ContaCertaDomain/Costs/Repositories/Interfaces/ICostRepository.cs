using ContaCerta.Domain.Common.Interfaces;
using ContaCerta.Domain.Costs.Model;

namespace ContaCerta.Domain.Costs.Repositories.Interfaces
{
    public interface ICostRepository : IRepository<Cost>
    {
        public Cost[] NextCostsCreatedByUserId(int UserId);
        public Cost[] LastCostsCreatedByUserId(int UserId, int LastDays);
    }
}
