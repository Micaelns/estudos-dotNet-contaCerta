using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Costs.Services
{
    public class AddUsers
    {
        private readonly IUserCostRepository _userCostRepository;

        public AddUsers(IUserCostRepository userCostRepository)
        {
            _userCostRepository = userCostRepository;
        }

        public void Execute(Cost cost, User[] users)
        {
            if (users.Length == 0)
            {
                throw new ArgumentException("Lista de usuários sem dados");
            }

            User[] distinctUsers = users.Distinct().ToArray();
            UserCost[] userCostsSaved = _userCostRepository.ListUserCostsByCost(cost);

            if (userCostsSaved.Any(userCost => userCost.Payed))
            {
                throw new ArgumentException("Após algum pagamento não é possível adicionar usuários");
            }

            float individualCost = cost.Value / (distinctUsers.Length + userCostsSaved.Length);
            foreach (var user in distinctUsers)
            {
                if (!userCostsSaved.Any(userCost => userCost.User.Id == user.Id))
                {
                    var userCost = new UserCost(user, cost, individualCost);
                    _userCostRepository.Save(userCost);
                }
            }

            RecalculateCurrentCosts(userCostsSaved, individualCost);
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
}
