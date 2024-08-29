
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Validates.Interfaces;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Repositories.Interfaces;
using System;

namespace ContaCerta.Domain.Costs.Services
{
    public class UpdateCosts
    {
        private readonly ICostRepository _costRepository;
        private readonly ICostValidate _costValidate;

        public UpdateCosts(ICostRepository costRepository, IUserCostRepository userCostRepository, ICostValidate costValidate)
        {
            _costRepository = costRepository;
            _costValidate = costValidate;
        }

        public Cost Execute(Cost cost, string? title, string? description, float? value, DateTime? paymentDate, bool? active)
        {
            cost = DataEdit(cost, title, description, value, paymentDate, active);

            if (!_costValidate.IsValid(cost))
            {
                throw new ArgumentException("Custo inválido: \n - " + string.Join("\n - ", _costValidate.Messages));
            }

            try
            {
                return _costRepository.Save(cost);
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao atualizar custo. \n - " + e.Message);
            }
        }

        private Cost DataEdit(Cost cost, string? title, string? description, float? value, DateTime? paymentDate, bool? active)
        {
            bool hasEdited = false;

            if (title != null)
            {
                cost.Title = title;
                hasEdited = true;
            }

            if (description != null)
            {
                cost.Description = description;
            }

            if (value != null)
            {
                cost.Value = (float)value;
            }

            if (paymentDate != null)
            {
                cost.PaymentDate = paymentDate;
            }

            if (active != null)
            {
                cost.Active = (bool)active;

            }

            if (!hasEdited)
            {
                throw new Exception("Não há informação a ser editada. ");
            }

            return cost;
        }
    }
}
