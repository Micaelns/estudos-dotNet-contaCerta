using ContaCerta.Aplication.Common;
using ContaCerta.Aplication.Users.DTOs;
using ContaCerta.Aplication.Users.Requests;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Services;

namespace ContaCerta.Aplication.Users;

public class UserApp(ManagerUser managerUser, ListCostsUser listUsers)
{
    private readonly ManagerUser _managerUser = managerUser;
    private readonly ListCostsUser _listUsers = listUsers;

    public Response Create(UserCreateRequest request)
    {
        try
        {
            var data = _managerUser.Create(request.Email, request.Password);
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

    public Response ListActives()
    {
        try
        {
            var data = _managerUser.ListActives();
            return GenereteResponseUser(data);
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
            var costs = _listUsers.ListCostsLastDays(LoggedUser);
            if (costs.Length == 0)
                return new Response() { Status = StatusCode.NoContent };

            return GenereteResponseUserCost(costs);
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

    private Response GenereteResponseUserCost(UserCost[] userCosts)
    {
        return new ResponseList<UserCostDTO>()
        {
            Status = userCosts.Length == 0 ? StatusCode.NoContent : StatusCode.Success,
            Data = userCosts.Select(userCost => new UserCostDTO
            {
                User = new() { 
                    Email = userCost.User.Email,
                    Active = userCost.User.Active
                },
                Cost = new() {
                    Id = userCost.Cost.Id,
                    Title = userCost.Cost.Title,
                    Value = userCost.Cost.Value,
                    PaymentDate = userCost.Cost.PaymentDate,
                    Active = userCost.Cost.Active
                },
                Paid_at = userCost.Paid_at,
                Paid = userCost.Paid,
                Value = userCost.Value
            })
        };
    }

    private Response GenereteResponseUser(User[] users)
    {
        return new ResponseList<UserDTO>()
        {
            Status = StatusCode.Success,
            Data = users.Select(user => new UserDTO { 
                Email = user.Email,
                Active = user.Active
            })
        };
    }

    private Response GenereteSingleResponse(User user)
    {
        return new ResponseSingle<UserDTO>()
        {
            Status = StatusCode.Success,
            Data = new()
            {
                Email = user.Email,
                Active = user.Active
            }
        };
    }
}
