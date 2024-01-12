using ContaCerta.Domains.Users.Model;
using ContaCerta.Domain.Common.Interfaces;

namespace ContaCerta.Domains.Users.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public User[] ListActives();
        public User FindByEmail(string email);
    }
}