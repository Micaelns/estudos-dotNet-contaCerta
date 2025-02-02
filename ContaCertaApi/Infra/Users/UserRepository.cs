using ContaCerta.Api.Infra.Context;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Api.Infra.Users
{
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

        public User Find(int Id)
        {
            throw new NotImplementedException();
        }

        public User? FindByEmail(string email)
        {
            return _context.Users.Where(c => c.Email.Contains(email)).FirstOrDefault();
        }

        public User[] ListActives()
        {
            return _context.Users.Where(c => c.Active).ToArray<User>();
        }

        public User Save(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
