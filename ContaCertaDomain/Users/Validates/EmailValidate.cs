using System.Text.RegularExpressions;
using ContaCerta.Domain.Common.Interfaces;

namespace ContaCerta.Domains.Users.Validates
{
    public class EmailValidate: IValidate
    {
        private List<string> _messages = new List<string>();
        public IReadOnlyList<string> Messages => _messages.AsReadOnly();

        public virtual bool Execute(string email)
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