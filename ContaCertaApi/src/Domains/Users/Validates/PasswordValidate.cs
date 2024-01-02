using ContaCertaApi.Domains.Interfaces;

namespace ContaCertaApi.src.Domains.Users.Validates
{
    public class PasswordValidate: IValidate
    {
        private List<string> _messages = new List<string>();
        public IReadOnlyList<string> Messages => _messages.AsReadOnly();

        private int MinLength { get => 8; }
        public virtual bool Execute(string password)
        {
            _messages.Clear();

            if (string.IsNullOrWhiteSpace(password))
            {
                _messages.Add("A senha não pode ser vazia.");
                return false;
            }

            if (password.Length < MinLength)
            {
                _messages.Add("A senha deve ter mais de "+MinLength+" caracteres.");
            }

            if (!password.Any(char.IsUpper))
            {
                _messages.Add("A senha deve ter pelo menos uma letra maiúscula.");
            }

            if (!password.Any(char.IsNumber))
            {
                _messages.Add("A senha deve ter pelo menos um número.");
            }

            return Messages.Count == 0;
        }
    }
}