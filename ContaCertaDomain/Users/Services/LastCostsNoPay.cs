using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;

namespace ContaCerta.Domain.Users.Services
{
    public class LastCostsNoPay
    {
        private readonly IUserCostRepository _userCostRepository;

        public LastCostsNoPay(IUserCostRepository userCostRepository)
        {
            _userCostRepository = userCostRepository;
        }

        public UserCost[] Execute(User user)
        {
            if (user == null || user.Id <= 0)
            {
                throw new ArgumentException("Usuário inválido");
            }

            try
            {
                return _userCostRepository.LastUserCostNoPayByUser(user);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na consulta dos custos antigos não pagos por usuário. \n - " + e.Message);
            }
        }

    }
}
