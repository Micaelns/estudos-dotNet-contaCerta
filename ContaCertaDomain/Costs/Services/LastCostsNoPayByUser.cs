using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Services
{
    public class LastCostsNoPayByUser
    {
        private readonly ICostRepository _costRepository;

        public LastCostsNoPayByUser(ICostRepository costRepository)
        {
            _costRepository = costRepository;
        }

        public Cost[] Execute(User user)
        {
            if (user == null || user.Id <= 0)
            {
                throw new ArgumentException("Usuário inválido");
            }

            try
            {
                return _costRepository.LastCostsNoPayByUserId(user.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na consulta dos custos antigos não pagos por usuário. \n - " + e.Message);
            }
        }

    }
}
