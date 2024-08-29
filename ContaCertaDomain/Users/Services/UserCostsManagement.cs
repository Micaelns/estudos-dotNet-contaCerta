using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Helpers;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Users.Services;

public class UserCostsManagement
{
    protected readonly IUserCostRepository _userCostRepository;

    public UserCostsManagement(IUserCostRepository userCostRepository)
    {
        _userCostRepository = userCostRepository;
    }

    private void SaveAll(UserCost[] userCosts, float individualCost)
    {
        foreach (UserCost uCost in userCosts)
        {
            uCost.Value = individualCost;
            _userCostRepository.Save(uCost);
        }
    }

    private void DeleteAll(UserCost[] userCosts)
    {
        foreach (UserCost uCost in userCosts)
        {
            _userCostRepository.Delete(uCost);
        }
    }

    public void SaveUsersCosts(Cost cost, User[] addUsers, User[] removeUsers)
    {
        UserCost[] userCostsSaved = _userCostRepository.ListUserCostsByCost(cost);

        if (UserCostHelper.ExistsAnyPayment(userCostsSaved))
        {
            throw new ArgumentException("Após algum pagamento não é possível adicionar usuários");
        }

        UserCost[] allUserCosts = PrepareUserCost(cost, userCostsSaved, addUsers);
        UserCost[] toRemoveUserCosts = allUserCosts.Length > 0 ? allUserCosts.Where(userCost => removeUsers.Contains(userCost.User)).ToArray() : [];
        UserCost[] userCosts = allUserCosts.Except(toRemoveUserCosts).ToArray();

        if (userCosts.Length > 0)
        {
            float individualCost = UserCostHelper.CalculateIndividualCost(cost, userCosts.Length);
            SaveAll(userCosts, individualCost);
        }

        if (toRemoveUserCosts.Length > 0)
        {
            DeleteAll(toRemoveUserCosts);
        }
    }

    private UserCost[] PrepareUserCost(Cost cost, UserCost[] userCosts, User[] addUsers)
    {
        User[] usersSaved =
        [
            .. userCosts.Aggregate(new List<User>(), (acc, userCost) =>
            {
                acc.Add(userCost.User);
                return acc;
            }),
        ];

        User[] usersNotSaved = addUsers.Distinct().Where(item => !usersSaved.Contains(item)).ToArray();

        return
        [
            .. userCosts,
            .. usersNotSaved.Select(user =>
            {
                var userCost = new UserCost(user, cost);
                return userCost;
            }
            ).ToArray(),
        ];
    }
}
