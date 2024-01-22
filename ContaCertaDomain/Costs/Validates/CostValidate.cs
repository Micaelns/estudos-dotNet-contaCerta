using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Validates.Interfaces;

namespace ContaCerta.Domain.Costs.Validates
{
    public class CostValidate : ICostValidate
    {
        private List<string> _messages = new List<string>();
        public IReadOnlyList<string> Messages => _messages.AsReadOnly();

        public bool IsValid(Cost cost)
        {
            _messages.Clear();

            if (string.IsNullOrWhiteSpace(cost.Title))
            {
                _messages.Add("O titulo do custo não pode ser vazio.");
                return false;
            }

            if (cost.UserRequested == null)
             {
                _messages.Add("O usuário deve ser válido.");
            }

            if (cost.Value < 0) { 
                _messages.Add("O valor não pode ser negativo."); 
            }

            return Messages.Count == 0;
        }
    }
}
