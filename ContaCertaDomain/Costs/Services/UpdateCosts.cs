
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Validates.Interfaces;
using ContaCerta.Domain.Users.Helpers;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;

namespace ContaCerta.Domain.Costs.Services;
public class UpdateCosts : UserCostsManagement
{
    private readonly ICostRepository _costRepository;
    private readonly ICostValidate _costValidate;

    public UpdateCosts(ICostRepository costRepository, 
                        ICostValidate costValidate, 
                        IUserCostRepository userCostRepository): base(userCostRepository)
    {
        _costRepository = costRepository;
        _costValidate = costValidate;
    }

    public Cost Execute(Cost cost, string? title, string? description, float? value, DateTime? paymentDate, bool? active)
    {
        if (ExistsAnyPayment(cost))
        {
            throw new ArgumentException("Após algum pagamento não é possível realizar alterações");
        }

        Cost? costEdited = GetDataEdited(cost, title, description, value, paymentDate, active);

        if (costEdited == null)
        {
            return cost;
        }
        
        if (!_costValidate.IsValid(costEdited))
        {
            throw new ArgumentException("Custo inválido: \n - " + string.Join("\n - ", _costValidate.Messages));
        }

        try
        {
            var edited = _costRepository.Save(costEdited);
            SaveUsersCosts(cost, [], []);
            return edited;
        }
        catch (Exception e)
        {
            _costRepository.Save(cost);
            throw new Exception("Erro ao atualizar custo. \n - " + e.Message);
        }
    }

    private bool ExistsAnyPayment(Cost cost)
    {
        UserCost[] userCosts = _userCostRepository.ListUserCostsByCost(cost);

        return UserCostHelper.ExistsAnyPayment(userCosts);
    }

    private Cost? GetDataEdited(Cost cost, string? title, string? description, float? value, DateTime? paymentDate, bool? active)
    {
        if (title == null && description == null && value == null && paymentDate == null && active == null)
        {
            return null;
        }

        string newTitle = title ?? cost.Title;
        float newValue = value ?? cost.Value;
        bool newActive = active ?? cost.Active;

        if (!cost.Active)
        {
            newValue = cost.Value;
        }

        return new Cost(newTitle, description, newValue, paymentDate, cost.UserRequested, newActive)
        {
            Id = cost.Id,
            CreatedAt = cost.CreatedAt
        };
    }
}
