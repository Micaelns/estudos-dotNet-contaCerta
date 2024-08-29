using ContaCerta.Api.Infra.Context;
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContaCerta.Api.Infra.Costs
{
    public class CostRepository : ICostRepository
    {
        private readonly ContaCertaContext _context;
        public CostRepository(ContaCertaContext context)
        {
            _context = context;
        }

        public Cost? Find(int Id)
        {
            return _context.Costs.Include(c => c.UserRequested).FirstOrDefault(c => c.Id == Id);
        }

        public Cost[] LastCostsCreatedByUserId(int UserId, int LastDays)
        {
            return _context.Costs.Where(c => c.UserRequested.Id == UserId && ( c.PaymentDate >= System.DateTime.Now.AddDays(-LastDays) && c.PaymentDate <= System.DateTime.Now)).ToArray();
        }

        public Cost[] NextCostsCreatedByUserId(int UserId)
        {
            return _context.Costs.Where(c => c.UserRequested.Id == UserId && (c.PaymentDate == null || c.PaymentDate >= System.DateTime.Now)).ToArray();
        }

        public Cost Save(Cost entity)
        {
            if (entity.Id <= 0)
            {
                _context.Add(entity);
            }
            else
            {
                _context.Update(entity);
            }
            _context.SaveChanges();
            return entity;
        }
    }
}
