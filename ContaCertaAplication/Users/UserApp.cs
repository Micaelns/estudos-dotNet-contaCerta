using ContaCerta.Aplication.Common;
using ContaCerta.Aplication.Users.DTOs;
using ContaCerta.Aplication.Users.Requests;
using ContaCerta.Domain.Users.Model;
using ContaCerta.Domain.Users.Services;

namespace ContaCerta.Aplication.Users;

public class UserApp(ManagerUser managerUser)
{
    private readonly ManagerUser _managerUser = managerUser;

    public Response Create(UserCreateRequest request)
    {
        try
        {
            var data = _managerUser.Create(request.Email, request.Password, request.Nickname, request.IsPublicEmail);
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
            return GenereteResponseUser(data.ToArray());
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

    private Response GenereteResponseUser(User[] users)
    {
        if (users.Length == 0)
            return new Response() { Status = StatusCode.NoContent };

        return new ResponseList<UserDTO>()
        {
            Status = StatusCode.Success,
            Data = users.Select(user => new UserDTO {
                Nickname = user.NickName,
                Email = user.IsPublicEmail ? user.Email : "********",
                Active = user.Active
            }),
            Pagination = new()
            {
                CurrentPage = 1,
                PerPage = 100,
                TotalPages = 1,
                TotalItems = users.Length,
            }
        };
    }

    private Response GenereteSingleResponse(User user)
    {
        return new ResponseSingle<UserDTO>()
        {
            Status = StatusCode.Success,
            Data = new()
            {
                Nickname = user.NickName,
                Email = user.IsPublicEmail? user.Email : "********",
                Active = user.Active
            }
        };
    }
}
