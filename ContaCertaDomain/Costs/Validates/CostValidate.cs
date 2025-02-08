using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Validates.Interfaces;

namespace ContaCerta.Domain.Costs.Validates;

public class CostValidate : ICostValidate
{
    private List<string> _messages = new List<string>();
    public string ErrorMessages { 
        get => _messages.Count == 0 ? string.Empty : "\n - " + string.Join("\n - ", _messages);
    }

    public bool IsValid(Cost cost)
    {
        _messages.Clear();

        if (string.IsNullOrWhiteSpace(cost.Title))
        {
            _messages.Add(MessageCost.TitleCanNotEmpty);
            return false;
        }

        if (cost.UserRequested  is null || cost.UserRequested.Active is false)
        {
            _messages.Add(MessageCost.UserCanNotInvalid);
        }

        if (cost.Value < 0) { 
            _messages.Add(MessageCost.ValueCanNotNegative); 
        }

        return _messages.Count == 0;
    }
}
