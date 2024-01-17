using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories;

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