using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Services
{
    public class MyLastCosts
    {
        private readonly ICostRepository _costRepository;

        public MyLastCosts(ICostRepository costRepository)
        {
            _costRepository = costRepository;
        }

        public Cost[] Execute(User user, int lastDays = 15)
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
                return _costRepository.LastCostsByUserId(user.Id, lastDays);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na consulta dos ultimos "+ lastDays + " custos. \n - " + e.Message);
            }
        }
    }
}
