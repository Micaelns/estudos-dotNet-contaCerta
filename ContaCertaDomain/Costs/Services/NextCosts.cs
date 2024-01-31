using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Services
{
    public class NextCosts
    {
        private readonly ICostRepository _costRepository;

        public NextCosts(ICostRepository costRepository)
        {
            _costRepository = costRepository;
        }

        public Cost[] Execute(User user)
        {
            if (user == null || user.Active == false)
            {
                throw new ArgumentException("Usuário inválido ou inativo");
            }

            try
            {
                return _costRepository.NextCostsByUserId(user.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na consulta dos próximos custos. \n - " + e.Message);
            }
        }
    }
}
