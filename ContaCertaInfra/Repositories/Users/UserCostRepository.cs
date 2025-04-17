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

    public async Task Delete(int Id)
    {
        await _context.UserCosts.Where(c => c.Id == Id).ExecuteDeleteAsync();
        await _context.SaveChangesAsync();
    }

    public UserCost Find(int Id)
    {
        throw new NotImplementedException();
    }

    public UserCost[] LastUserCostNoPayByUser(User user)
    {
        throw new NotImplementedException();
    }

    public UserCost[] ListUserCostsByCost(Cost cost)
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
