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
            if (!cost.Active)
            {
                throw new ArgumentException("Custo inválido ou inativo");
            }

            if (users.Length == 0)
            {
                throw new ArgumentException("Lista de usuários sem dados");
            }

            UserCost[] userCostsSaved = _userCostRepository.ListUserCostsByCost(cost);

            if (userCostsSaved.Any(userCost => userCost.Payed))
            {
                throw new ArgumentException("Após algum pagamento não é possível adicionar usuários");
            }

            User[] usersSaved = userCostsSaved.Aggregate(new List<User>(), (acc, userCost) =>
            {
                acc.Add(userCost.User);
                return acc;
            }).ToArray();

            User[] distinctUsers = users.Union(usersSaved).Distinct().ToArray();

            float individualCost = (float) Math.Round((cost.Value / distinctUsers.Length), 2);
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
