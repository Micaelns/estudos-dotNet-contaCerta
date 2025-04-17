using ContaCerta.Domain.Common.Interfaces;
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Users.Repositories.Interfaces;

public interface IUserCostRepository : IRepository<UserCost>
{
    public UserCost[] ListUserCostsByCost(Cost cost);
    public UserCost[] LastUserCostNoPayByUser(User user);
}
