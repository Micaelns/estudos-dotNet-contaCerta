using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Costs.Services;

public class ManagerUsersInCost
{
    private readonly IUserCostRepository _userCostRepository;

    public ManagerUsersInCost(IUserCostRepository userCostRepository)
    {
        _userCostRepository = userCostRepository;
    }

    public void RemoveUsers(User[] users, Cost cost)
    {
        if (!ValidateListUserAndCost(users, cost))
        {
            return;
        }

        var userCostsSaved = _userCostRepository.ListUserCostsByCost(cost);
        if (userCostsSaved.Length == 0)
        {
            return;
        }

        if (userCostsSaved.Any(userCost => userCost.Payed))
        {
            throw new Exception(MessageCost.ImpossibleManagerUsersIfAnyPaid);
        }

        var distinctUsers = userCostsSaved.Length;
        var idsToRemove = new List<int>();
        foreach (var userCost in userCostsSaved)
        {
            if ( users.Any( u => u.Id == userCost.User.Id))
            {
                _userCostRepository.Delete(userCost.Id);
                idsToRemove.Add(userCost.Id);
                distinctUsers--;
            }
        }

        if (idsToRemove.Count == 0)
        {
            return;
        }

        userCostsSaved = userCostsSaved.Where(u => !idsToRemove.Contains(u.Id)).ToArray();
        float individualCost = CalculateIndividualValue(cost, distinctUsers);
        RecalculateCurrentCosts(userCostsSaved, individualCost);
    }

    public void AddUsers(User[] users, Cost cost)
    {
        if (!ValidateListUserAndCost(users, cost))
        {
            return;
        }

        UserCost[] userCostsSaved = _userCostRepository.ListUserCostsByCost(cost);

        if (userCostsSaved.Any(userCost => userCost.Payed))
        {
            throw new Exception(MessageCost.ImpossibleManagerUsersIfAnyPaid);
        }

        User[] usersSaved = userCostsSaved.Aggregate(new List<User>(), (acc, userCost) =>
        {
            acc.Add(userCost.User);
            return acc;
        }).ToArray();

        User[] distinctUsers = users.Union(usersSaved).Distinct().ToArray();

        float individualCost = CalculateIndividualValue(cost, distinctUsers.Length);
        var someSaved = SaveNewUserCosts(userCostsSaved, distinctUsers, cost, individualCost);
        if (someSaved)
        {
            RecalculateCurrentCosts(userCostsSaved, individualCost);
        }
    }

    private bool ValidateListUserAndCost(User[] users, Cost cost)
    {
        if (users.Length == 0)
        {
            return false;
        }

        if (!cost.Active)
        {
            throw new ArgumentException(MessageCost.InvalidCost);
        }

        return true;
    }

    private float CalculateIndividualValue(Cost cost, int qtdUsers)
    {
        return (float)Math.Round((cost.Value / qtdUsers), 2);
    }

    private bool SaveNewUserCosts(UserCost[] userCostsSaved, User[] distinctUsers, Cost cost, float individualCost)
    {
        bool someSaved = false;
        foreach (var user in distinctUsers)
        {
            if (!userCostsSaved.Any(userCost => userCost.User.Id == user.Id))
            {
                var userCost = new UserCost(user, cost, individualCost);
                _userCostRepository.Save(userCost);
                someSaved = true;
            }
        }
        return someSaved;
    }

    private void RecalculateCurrentCosts(UserCost[] userCosts, float individualCost)
    {
        if (userCosts.Length == 0)
        {
            return;
        }

        foreach (var userCost in userCosts)
        {
            userCost.Value = individualCost;
            _userCostRepository.Save(userCost);
        }
    }
}
