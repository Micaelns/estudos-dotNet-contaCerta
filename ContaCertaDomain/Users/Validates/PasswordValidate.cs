using ContaCerta.Domain.Users.Validates.Interfaces;

namespace ContaCerta.Domain.Users.Validates;

public class PasswordValidate: IPasswordValidate
{
    private List<string> _messages = new();
    public string ErrorMessages {
        get => _messages.Count == 0 ? string.Empty : "\n - " + string.Join("\n - ", _messages);
    }
    public int MinLength { get => 8; }

    public bool IsValid(string password)
    {
        _messages.Clear();

        if (string.IsNullOrWhiteSpace(password))
        {
            _messages.Add(MessageUser.InvalidPasswordEmpty);
            return false;
        }

        if (password.Length < MinLength)
        {
            var preparedMessage = MessageUser.InvalidPasswordSort.Replace("{0}", MinLength.ToString());
            _messages.Add(preparedMessage);
        }

        if (!password.Any(char.IsLower))
        {
            _messages.Add(MessageUser.InvalidPasswordNoLowerCase);
        }

        if (!password.Any(char.IsUpper))
        {
            _messages.Add(MessageUser.InvalidPasswordNoUpperCase);
        }

        if (!password.Any(char.IsNumber))
        {
            _messages.Add(MessageUser.InvalidPasswordNoNumbers);
        }

        return _messages.Count == 0;
    }
}