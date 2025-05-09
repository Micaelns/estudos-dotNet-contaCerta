using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Common.Interfaces;

namespace ContaCerta.Domain.Users.Repositories.Interfaces;

public interface IUserRepository : IRepository<User>
{
    public IEnumerable<User> ListActives();
    public User? FindByEmail(string email);
}