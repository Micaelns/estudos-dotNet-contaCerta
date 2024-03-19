using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Users.Services
{
    public class LastCosts
    {
        private readonly IUserCostRepository _userCostRepository;

        public LastCosts(IUserCostRepository userCostRepository)
        {
            _userCostRepository = userCostRepository;
        }

        public UserCost[] Execute(User user, int lastDays = 15)
        {
            if (lastDays < 0)
            {
                throw new ArgumentException("O número de dias deve ser maior ou igual a zero");
            }

            if (user == null || user.Active == false)
            {
                throw new ArgumentException("Usuário inválido ou inativo");
            }

            try
            {
                var userCosts = _userCostRepository.LastUserCostsByUser(user, lastDays);
                return userCosts;
            }
            catch (Exception e)
            {
                throw new Exception("Erro na consulta dos ultimos " + lastDays + " custos. \n - " + e.Message);
            }
        }
    }
}
