﻿using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Repositories.Interfaces;
using ContaCerta.Domain.Costs.Validates.Interfaces;
using ContaCerta.Domain.Users;
using ContaCerta.Domain.Users.Model;

namespace ContaCerta.Domain.Costs.Services;

public class ManagerCost
{
    private readonly ICostRepository _costRepository;
    private readonly ICostValidate _costValidate;

    public ManagerCost(ICostRepository costRepository, ICostValidate costValidate)
    {
        _costRepository = costRepository;
        _costValidate = costValidate;
    }

    public Cost Create(string title, string? description, float value, DateTime? paymentDate, User userRequested, bool active)
    {
        var cost = new Cost()
        {
            Title = title,
            Description = description,
            Value = value,
            PaymentDate = paymentDate,
            UserRequested = userRequested,
            Active = active
        };
        if (!_costValidate.IsValid(cost))
        {
            throw new ArgumentException(_costValidate.ErrorMessages);
        }

        try
        {
            return _costRepository.Save(cost);
        } catch (Exception e)
        {
            throw new Exception(MessageCost.ErrorSave+": \n - " +e.Message);
        }
    }

    public Cost Find(int id)
    {
        if (id < 1)
        {
            throw new ArgumentException(MessageCost.InvalidCost);
        }

        try
        {
            var cost = _costRepository.Find(id);
            if (cost == null)
            {
                throw new Exception(MessageCost.InvalidCost);
            }
            return cost;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public IEnumerable<Cost> LastCostsByUser(User user, int lastDays = 15)
    {
        if (lastDays < 0)
        {
            throw new ArgumentException(MessageCost.InvalidNumberOfDays);
        }

        if (user.Active is false)
        {
            throw new ArgumentException(MessageUser.InvalidUser);
        }

        try
        {
           return _costRepository.LastCostsUserId(user.Id, lastDays);
        }
        catch (Exception e)
        {
            var preparedMessage = MessageCost.ErrorLastDaysQuery.Replace("{0}", lastDays.ToString());
            throw new Exception(preparedMessage + " \n - " + e.Message);
        }
    }

    public IEnumerable<Cost> NextCostsByUser(User user)
    {
        if (user.Active is false)
        {
            throw new ArgumentException(MessageUser.InvalidUser);
        }

        try
        {
            return _costRepository.NextCostsUserId(user.Id);
        }
        catch (Exception e)
        {
            throw new Exception(MessageCost.ErrorNextDaysQuery + " \n - " + e.Message);
        }
    }

}
