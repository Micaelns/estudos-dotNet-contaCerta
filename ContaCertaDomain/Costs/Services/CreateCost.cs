using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Services
{
    public class CreateCost
    {
        private readonly ICostRepository _costRepository;

        public CreateCost(ICostRepository costRepository)
        {
            _costRepository = costRepository;
        }

        public Cost Execute(string title, string description, float value, User userRequested, bool active)
        {
            try
            {
                var cost = new Cost(title, description, value, userRequested, active);
                return _costRepository.Save(cost);
            } catch (Exception e)
            {
                throw new Exception("Erro ao salvar custo. \n - "+e.Message);
            }
        }
    }
}
