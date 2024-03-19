using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;

namespace ContaCerta.Domain.Costs.Services
{
    public class FindCost
    {
        private readonly ICostRepository _costRepository;

        public FindCost(ICostRepository costRepository)
        {
            _costRepository = costRepository;
        }

        public Cost Execute(int id)
        {
            if (id < 1)
            {
                throw new ArgumentException("Código do Custo inválido");
            }

            try
            {
                var cost = _costRepository.Find(id);
                if (cost == null)
                {
                    throw new Exception("Custo não encontrado");
                }
                return cost;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
