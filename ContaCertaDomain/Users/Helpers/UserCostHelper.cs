using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Users.Helpers;

public class UserCostHelper
{
    public static bool ExistsAnyPayment(UserCost[] userCosts) => userCosts.Any(userCost => userCost.Payed);

    public static float CalculateIndividualCost(Cost cost, int registersCount)
    {
        return (float)Math.Round(cost.Value / registersCount, 2);
    }
}
