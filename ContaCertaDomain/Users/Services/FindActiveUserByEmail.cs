using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Validates.Interfaces;

namespace ContaCerta.Domain.Users.Services
{
    public class FindActiveUserByEmail
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailValidate _emailValidate;
        public FindActiveUserByEmail(IUserRepository userRepository, IEmailValidate emailValidate)
        {
            _userRepository = userRepository;
            _emailValidate = emailValidate;
        }

        public User Execute(string email)
        {
            if (!_emailValidate.IsValid(email))
            {
                throw new ArgumentException("E-mail inválido");
            }
            User? user;
            try
            {
                user = _userRepository.FindByEmail(email);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao consultar usuário. \n - " + e.Message);
            }
            if (user == null)
            {
                throw new ArgumentException("Usuário não encontrado");
            }
            if (!user.Active)
            {
                throw new ArgumentException("Usuário inativo no momento");
            }
            return user;
        }
    }
}
