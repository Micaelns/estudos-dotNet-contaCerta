using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Infra.Context;

namespace ContaCerta.Infra.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly ContaCertaContext _context;
    public UserRepository(ContaCertaContext context)
    {
        _context = context;
    }

    public void Delete(int Id)
    {
        Console.WriteLine($"Deletado ID: {Id}");
    }

    public User? Find(int Id)
    {
        return _context.Users.Where(c => c.Id == Id).FirstOrDefault();
    }

    public User? FindByEmail(string email)
    {
        return _context.Users.Where(c => c.Email.Contains(email)).FirstOrDefault();
    }

    public User[] ListActives()
    {
        return _context.Users.Where(c => c.Active).ToArray();
    }

    public User Save(User entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
        return entity;
    }
}
