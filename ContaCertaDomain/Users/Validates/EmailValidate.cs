using ContaCerta.Domain.Users.Validates.Interfaces;
using System.Text.RegularExpressions;

namespace ContaCerta.Domain.Users.Validates
{
    public class EmailValidate: IEmailValidate
    {
        private List<string> _messages = new List<string>();
        public IReadOnlyList<string> Messages => _messages.AsReadOnly();

        public bool IsValid(string email)
        {
            _messages.Clear();

            string emailPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            bool isValid = Regex.IsMatch(email, emailPattern);

            if(!isValid) {
                _messages.Add("E-mail inválido");
                return false;
            }

            return true;
        }
    }
}