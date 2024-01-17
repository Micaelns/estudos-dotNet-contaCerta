using ContaCerta.Domain.Users.Validates.Interfaces;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories;

namespace ContaCerta.Domain.Users.Services
{
    public class CreateUser
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailValidate _emailValidate;
        private readonly IPasswordValidate _passwordValidate;
        
        public CreateUser(IUserRepository userRepository, IEmailValidate emailValidate, IPasswordValidate passwordValidate)
        {
            _userRepository = userRepository;
            _emailValidate = emailValidate;
            _passwordValidate = passwordValidate;
        }

        public User Execute(string email, string password)
        {
            if (!_emailValidate.IsValid(email))
            {
                throw new ArgumentException("E-mail inválido");
            }

            if (!_passwordValidate.IsValid(password))
            {
                throw new ArgumentException("Senha inválida: \n - "+string.Join("\n - ", _passwordValidate.Messages));
            }

            if (_userRepository.FindByEmail(email) != null)
            {
                throw new ArgumentException("Email já cadastrado");
            }

            try {
                var user = new User(email,  password, true);
                return _userRepository.Save(user);
            } catch (Exception e)
            {
                throw new Exception("Erro ao salvar usuário. \n - "+e.Message);
            }
        }
    }
}