using ContaCerta.Domain.Common.Interfaces;
using ContaCerta.Domain.Costs.Model;

namespace ContaCerta.Domain.Costs.Repositories.Interfaces
{
    public interface ICostRepository : IRepository<Cost>
    {
        public Cost[] NextCostsByUserId(int UserId);
        public Cost[] NextCostsCreatedByUserId(int UserId);
        public Cost[] LastCostsByUserId(int UserId, int LastDays);
        public Cost[] LastCostsCreatedByUserId(int UserId, int LastDays);
        public Cost[] LastCostsNoPayByUserId(int UserId);
    }
}
