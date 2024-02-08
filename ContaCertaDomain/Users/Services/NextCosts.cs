using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Users.Services
{
    public class NextCosts
    {
        private readonly IUserCostRepository _userCostRepository;

        public NextCosts(IUserCostRepository userCostRepository)
        {
            _userCostRepository = userCostRepository;
        }

        public UserCost[] Execute(User user)
        {
            if (user == null || user.Active == false)
            {
                throw new ArgumentException("Usuário inválido ou inativo");
            }

            try
            {
                return _userCostRepository.NextUserCostByUser(user);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na consulta dos próximos custos. \n - " + e.Message);
            }
        }
    }
}
