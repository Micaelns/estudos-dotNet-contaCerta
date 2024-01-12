using ContaCerta.Domains.Users.Model;
using ContaCerta.Domains.Users.Repositories;

namespace ContaCerta.src.Domains.Users.Services
{
    public class ListUsersUsersActives
    {
        private readonly IUserRepository _userRepository;
        public ListUsersUsersActives(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public User[] Execute()
        {
            return _userRepository.ListActives();
        }
    }
}