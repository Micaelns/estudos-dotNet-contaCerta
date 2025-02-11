using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace ContaCerta.Infra.Repositories.Users;

public class UserCostRepository : IUserCostRepository
{
    private readonly ContaCertaContext _context;
    public UserCostRepository(ContaCertaContext context)
    {
        _context = context;
    }

    public void Delete(int Id)
    {
        Console.WriteLine($"Deletado ID: {Id}");
    }

    public UserCost Find(int Id)
    {
        throw new NotImplementedException();
    }

    public UserCost[] LastUserCostNoPayByUser(User user)
    {
        throw new NotImplementedException();
    }

    public UserCost[] LastUserCostsByUser(User user, int lastDays)
    {
        return _context.UserCosts.Where(c => c.User.Id == user.Id && c.Cost.CreatedAt >= DateTime.Now.AddDays(-lastDays))
            .Include(uc => uc.Cost)
            .ToArray();
    }

    public UserCost[] ListUserCostsByCost(Cost cost)
    {
        return _context.UserCosts.Where(c => c.Cost.Id == cost.Id).ToArray();
    }

    public UserCost[] NextUserCostByUser(User user)
    {
        throw new NotImplementedException();
    }

    public UserCost Save(UserCost entity)
    {
        if (entity.Id <= 0)
        {
            _context.Add(entity);
        }
        else
        {
            var entityAtual = _context.UserCosts.Find(entity.Id) ?? throw new ArgumentException("Custo Id => " + entity.Id + " inválido");

            entityAtual.Value = entity.Value;
            _context.Update(entityAtual);
        }
        _context.SaveChanges();
        return entity;
    }
}
