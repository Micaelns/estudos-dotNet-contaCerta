using ContaCerta.Domain.Users.Validates.Interfaces;
using System.Text.RegularExpressions;

namespace ContaCerta.Domain.Users.Validates;

public class EmailValidate: IEmailValidate
{
    private List<string> _messages = new();
    public string ErrorMessages {
        get => _messages.Count == 0 ? string.Empty : " - " + string.Join(" - ", _messages);
    }

    public bool IsValid(string email)
    {
        _messages.Clear();

        string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
        bool isValid = Regex.IsMatch(email, emailPattern);

        if(!isValid) {
            _messages.Add(MessageUser.InvalidEmail);
            return false;
        }

        return true;
    }
}