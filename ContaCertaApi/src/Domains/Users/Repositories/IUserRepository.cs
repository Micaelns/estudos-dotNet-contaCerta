using ContaCertaApi.Domains.Users.Model;
using ContaCertaApi.Domains.Interfaces;

namespace ContaCertaApi.Domains.Users.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        public User FindByEmail(string email);
    }
}