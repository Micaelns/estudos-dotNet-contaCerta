using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;

namespace ContaCerta.Domain.Costs.Repositories
{
    public class CostRepository : ICostRepository
    {
        public Cost Find(int Id)
        {
            throw new NotImplementedException();
        }

        public Cost[] LastCostsCreatedByUserId(int UserId, int LastDays)
        {
            throw new NotImplementedException();
        }

        public Cost[] NextCostsCreatedByUserId(int UserId)
        {
            throw new NotImplementedException();
        }

        public Cost Save(Cost entity)
        {
            return entity;
        }
    }
}
