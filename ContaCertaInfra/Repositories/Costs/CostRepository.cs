using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Infra.Context;

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

    public Cost[] LastCostsCreatedByUserId(int UserId, int LastDays)
    {
        return _context.Costs.Where(c => c.UserRequested.Id == UserId && c.PaymentDate >= DateTime.Now.AddDays(-LastDays) && c.PaymentDate <= DateTime.Now).ToArray();
    }

    public Cost[] NextCostsCreatedByUserId(int UserId)
    {
        return _context.Costs.Where(c => c.UserRequested.Id == UserId && (c.PaymentDate == null || c.PaymentDate >= DateTime.Now)).ToArray();
    }

    public Cost Save(Cost entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        return entity;
    }
}
