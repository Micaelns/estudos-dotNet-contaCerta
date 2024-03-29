using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Users.Services
{
    public class ListActivesUsers
    {
        private readonly IUserRepository _userRepository;
        public ListActivesUsers(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public User[] Execute()
        {
            return _userRepository.ListActives();
        }
    }
}