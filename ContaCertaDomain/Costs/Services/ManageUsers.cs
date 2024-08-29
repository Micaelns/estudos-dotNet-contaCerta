using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using ContaCerta.Domain.Users.Services;

namespace ContaCerta.Domain.Costs.Services;

public class ManageUsers : UserCostsManagement
{

    public ManageUsers(IUserCostRepository userCostRepository): base(userCostRepository)
    {
    }

    public void Execute(Cost cost, User[] addUsers, User[] removeUsers)
    {
        if (!cost.Active)
        {
            throw new ArgumentException("Custo inativo");
        }

        SaveUsersCosts(cost, addUsers, removeUsers);
    }
}
