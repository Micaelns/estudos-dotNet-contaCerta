using ContaCerta.Domain.Costs;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Users.Services;

public class ListCostsUser
{
    private readonly IUserCostRepository _userCostRepository;

    public ListCostsUser(IUserCostRepository userCostRepository)
    {
        _userCostRepository = userCostRepository;
    }

    public UserCost[] ListCostsLastDays(User user, int lastDays = 15)
    {
        if (user.Active is false)
        {
            throw new ArgumentException(MessageUser.InvalidUser);
        }

        try
        {
            var userCosts = _userCostRepository.LastUserCostsByUser(user, Math.Abs(lastDays));
            return userCosts;
        }
        catch (Exception e)
        {
            var preparedMessage = MessageCost.ErrorLastDaysQuery.Replace("{0}", Math.Abs(lastDays).ToString());
            throw new Exception(preparedMessage + " \n - " + e.Message);
        }
    }

    public UserCost[] NextCosts(User user)
    {
        if ( user.Active is false)
        {
            throw new ArgumentException(MessageUser.InvalidUser);
        }

        try
        {
            return _userCostRepository.NextUserCostByUser(user);
        }
        catch (Exception e)
        {
            throw new Exception(MessageCost.ErrorNextDaysQuery + " \n - " + e.Message);
        }
    }

    public UserCost[] LastCostsNoPay(User user)
    {
        if (user.Active is false)
        {
            throw new ArgumentException(MessageUser.InvalidUser);
        }

        try
        {
            return _userCostRepository.LastUserCostNoPayByUser(user);
        }
        catch (Exception e)
        {
            throw new Exception(MessageCost.ErrorCostQueryNoPay + " \n - " + e.Message);
        }
    }
}
