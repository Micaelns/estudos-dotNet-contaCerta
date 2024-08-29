using ContaCerta.Api.Infra.Context;
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContaCerta.Api.Infra.Users
{
    public class UserCostRepository : IUserCostRepository
    {
        private readonly ContaCertaContext _context;
        public UserCostRepository(ContaCertaContext context)
        {
            _context = context;
        }
        public UserCost? Find(int Id)
        {
            return _context.UserCosts.FirstOrDefault(c => c.Id == Id);
        }

        public UserCost[] LastUserCostNoPayByUser(User user)
        {
            throw new NotImplementedException();
        }

        public UserCost[] LastUserCostsByUser(User user, int lastDays)
        {
            return _context.UserCosts.Where(c => c.User.Id == user.Id && c.Cost.CreatedAt >= System.DateTime.Now.AddDays(-lastDays))
                .Include(uc => uc.Cost)
                .ToArray<UserCost>();
        }

        public UserCost[] ListUserCostsByCost(Cost cost)
        {
            return _context.UserCosts.Where(c => c.Cost.Id == cost.Id ).ToArray<UserCost>();
        }

        public UserCost[] NextUserCostByUser(User user)
        {
            throw new NotImplementedException();
        }

        public UserCost Save(UserCost entity)
        {
            if (entity.Id <= 0 )
            {
                _context.Add(entity);
            } else {
                _context.Update(entity);
            }
            _context.SaveChanges();
            return entity;
        }

        public void Delete(UserCost entity)
        {
            //implementar
        }
    }
}
