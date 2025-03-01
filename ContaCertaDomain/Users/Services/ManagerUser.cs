using ContaCerta.Domain.Users.Validates.Interfaces;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Users.Services;

public class ManagerUser
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailValidate _emailValidate;
    private readonly IPasswordValidate _passwordValidate;
    
    public ManagerUser(IUserRepository userRepository, IEmailValidate emailValidate, IPasswordValidate passwordValidate)
    {
        _userRepository = userRepository;
        _emailValidate = emailValidate;
        _passwordValidate = passwordValidate;
    }

    public User FindActiveByEmail(string email)
    {
        if (!_emailValidate.IsValid(email))
        {
            throw new ArgumentException(_emailValidate.ErrorMessages);
        }

        User? user;
        try
        {
            user = _userRepository.FindByEmail(email);
        }
        catch (Exception e)
        {
            throw new Exception(MessageUser.ErrorFind + ": \n - " + e.Message);
        }

        if ( user is null || !user.Active)
        {
            throw new ArgumentException(MessageUser.InvalidUser);
        }

        return user;
    }

    public User Create(string email, string password)
    {
        if (!_emailValidate.IsValid(email))
        {
            throw new ArgumentException(_emailValidate.ErrorMessages);
        }

        if (!_passwordValidate.IsValid(password))
        {
            throw new ArgumentException(_passwordValidate.ErrorMessages);
        }

        if (_userRepository.FindByEmail(email) != null)
        {
            throw new ArgumentException(MessageUser.UserExists);
        }

        try {
            var user = new User() { 
                Email = email,
                Password = password,
                Active = true
            };
            return _userRepository.Save(user);
        } catch (Exception e)
        {
            throw new Exception(MessageUser.ErrorSave + ": \n - " + e.Message);
        }
    }

    public User[] ListActives()
    {
        return _userRepository.ListActives();
    }
}