using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.src.Domains.Users.Services
{
    public class ListUsersActives
    {
        private readonly IUserRepository _userRepository;
        public ListUsersActives(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public User[] Execute()
        {
            return _userRepository.ListActives();
        }
    }
}