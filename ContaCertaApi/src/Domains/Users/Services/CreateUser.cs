using ContaCertaApi.Domains.Users.Model;
using ContaCertaApi.Domains.Users.Repositories;
using ContaCertaApi.src.Domains.Users.Validates;

namespace ContaCertaApi.Domains.Users.Services
{
    public class CreateUser
    {
        private readonly IUserRepository _userRepository;
        private readonly EmailValidate _emailValidate;
        private readonly PasswordValidate _passwordValidate;
        
        public CreateUser(IUserRepository userRepository, EmailValidate emailValidate,  PasswordValidate passwordValidate)
        {
            _userRepository = userRepository;
            _emailValidate = emailValidate;
            _passwordValidate = passwordValidate;
        }

        public User Execute(string email, string password)
        {
            if (!_emailValidate.Execute(email))
            {
                throw new ArgumentException("Email inválido");
            }

            if (!_passwordValidate.Execute(password))
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