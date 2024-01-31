using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Services
{
    public class NextCostsCreatedByUser
    {
        private readonly ICostRepository _costRepository;

        public NextCostsCreatedByUser(ICostRepository costRepository)
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
                return _costRepository.NextCostsCreatedByUserId(user.Id);
            }
            catch (Exception e)
            {
                throw new Exception("Erro na consulta dos próximos custos criado por " + user.Email + ". \n - " + e.Message);
            }
        }
    }
}
