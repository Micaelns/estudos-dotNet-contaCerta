using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContaCerta.Infra.Repositories.Costs;

public class CostRepository : ICostRepository
{
    private readonly ContaCertaContext _context;
    public CostRepository(ContaCertaContext context)
    {
        _context = context;
    }

    public async Task Delete(int Id)
    {
        Console.WriteLine($"Deletado ID: {Id}");
        await Task.CompletedTask;
    }

    public Cost? Find(int Id)
    {
        return _context.Costs.Where(c => c.Id == Id).FirstOrDefault();
    }

    public IEnumerable<Cost> LastCostsUserId(int userId, int lastDays)
    {
        var dataInicial = DateTime.Now.AddDays(-lastDays);
        var dataFinal = DateTime.Now;
        
        return _context.Costs
            .Where(c =>
                (c.UserRequested.Id == userId || c.UserCosts.Any(u => u.User.Id == userId)) &&
                c.PaymentDate >= dataInicial &&
                c.PaymentDate <= dataFinal
            )
            .Include(c => c.UserCosts)
            .Include(c => c.UserRequested)
            .ToList();
    }

    public IEnumerable<Cost> NextCostsUserId(int userId)
    {
        var dataInicial = DateTime.Now;

        return _context.Costs
            .Where(c =>
                (c.UserRequested.Id == userId || c.UserCosts.Any(u => u.User.Id == userId)) &&
                c.PaymentDate >= dataInicial
            )
            .Include(c => c.UserCosts)
            .Include(c => c.UserRequested)
            .ToList();
    }

    public Cost Save(Cost entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        return entity;
    }
}
