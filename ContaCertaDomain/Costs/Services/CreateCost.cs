using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Validates.Interfaces;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Services
{
    public class CreateCost
    {
        private readonly ICostRepository _costRepository;
        private readonly ICostValidate _costValidate;

        public CreateCost(ICostRepository costRepository, ICostValidate costValidate)
        {
            _costRepository = costRepository;
            _costValidate = costValidate;
        }

        public Cost Execute(string title, string? description, float value, DateTime? paymentDate, User userRequested, bool active)
        {
            var cost = new Cost(title, description, value, paymentDate, userRequested, active);
            if (!_costValidate.IsValid(cost))
            {
                throw new ArgumentException("Custo inválido: \n - "+string.Join("\n - ", _costValidate.Messages));
            }

            try
            {
                return _costRepository.Save(cost);
            } catch (Exception e)
            {
                throw new Exception("Erro ao salvar custo. \n - "+e.Message);
            }
        }
    }
}
