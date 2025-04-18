using ContaCerta.Aplication.Common;
using ContaCerta.Aplication.Costs.DTOs;
using ContaCerta.Aplication.Costs.Requests;
using ContaCerta.Domain.Costs.Model;
using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Services;

namespace ContaCerta.Aplication.Costs;

public class CostApp(ManagerCost managerCost, ManagerUser managerUser, ManagerUsersInCost managerUsersInCost)
{
    private readonly ManagerCost _managerCost = managerCost;
    private readonly ManagerUser _managerUser = managerUser;
    private readonly ManagerUsersInCost _managerUsersInCost = managerUsersInCost;

    public Response Create(CostCreateRequest request)
    {
        try
        {
            var LoggedUser = _managerUser.FindActiveByEmail(request.Email);
            var data = _managerCost.Create(request.Title, request.Description, request.Value, request.PaymentDate, LoggedUser, request.Active);
            return GenereteSingleResponse(data);
        }
        catch (ArgumentException e)
        {
            return new Response() { Status = StatusCode.BadRequest, StatusMessage = e.Message };
        }
        catch (Exception e)
        {
            return new Response() { Status = StatusCode.InternalError, StatusMessage = e.Message };
        }
    }

    public Response UsersInCost(int costId, UsersInCostRequest emails)
    {
        try
        {
            var cost = _managerCost.Find(costId);
            var newUsers = emails.NewEmails.Select(_managerUser.FindActiveByEmail).ToArray();
            var removeUsers = emails.RemoveEmails.Select(_managerUser.FindActiveByEmail).ToArray();

            _managerUsersInCost.RemoveUsers(removeUsers, cost);
            _managerUsersInCost.AddUsers(newUsers, cost);

            return new Response() { Status = StatusCode.Created };
        }
        catch (ArgumentException e)
        {
            return new Response() { Status = StatusCode.BadRequest, StatusMessage = e.Message };
        }
        catch (Exception e)
        {
            return new Response() { Status = StatusCode.InternalError, StatusMessage = e.Message };
        }
    }

    public Response GetLastCostsByUser(string email)
    {
        try
        {
            var LoggedUser = _managerUser.FindActiveByEmail(email);
            var costs = _managerCost.LastCostsByUser(LoggedUser);
            return GenereteResponseCost(costs.ToArray());
        }
        catch (ArgumentException e)
        {
            return new Response() { Status = StatusCode.BadRequest, StatusMessage = e.Message };
        }
        catch (Exception e)
        {
            return new Response() { Status = StatusCode.InternalError, StatusMessage = e.Message };
        }
    }

    public Response GetNextCostsByUser(string email)
    {
        try
        {
            var LoggedUser = _managerUser.FindActiveByEmail(email);
            var costs = _managerCost.NextCostsByUser(LoggedUser);
            return GenereteResponseCost(costs.ToArray());
        }
        catch (ArgumentException e)
        {
            return new Response() { Status = StatusCode.BadRequest, StatusMessage = e.Message };
        }
        catch (Exception e)
        {
            return new Response() { Status = StatusCode.InternalError, StatusMessage = e.Message };
        }
    }
    
    private Response GenereteResponseCost(Cost[] costs)
    {
        if (costs.Length == 0)
            return new Response() { Status = StatusCode.NoContent };

        return new ResponseList<CostDTO>()
        {
            Status = StatusCode.Success,
            Data = costs.Select(cost => new CostDTO
            {
                Id = cost.Id,
                Title = cost.Title,
                Description = cost.Description,
                Active = cost.Active,
                Value = cost.Value,
                Summary = cost.UserCosts is null ? null : new SummaryUserCost()
                {
                    TotalUsers = cost.UserCosts.Count(),
                    CountPaid = cost.UserCosts.Sum( uc => uc.Paid ? 1 : 0 ),
                    TotalPaid = cost.UserCosts.Sum(uc => uc.Paid ? uc.Value : 0)
                },
                Owner = cost.UserRequested?.NickName,
                PaymentDate = cost.PaymentDate
            })
        };
    }

    private Response GenereteSingleResponse(Cost cost)
    {
        if (cost is null)
            return new Response() { Status = StatusCode.NoContent };

        return new ResponseSingle<CostDTO>()
        {
            Status = StatusCode.Success,
            Data = new()
            {
                Id = cost.Id,
                Title = cost.Title,
                Description = cost.Description,
                Active = cost.Active,
                Value = cost.Value,
                PaymentDate = cost.PaymentDate
            }
        };
    }
}
