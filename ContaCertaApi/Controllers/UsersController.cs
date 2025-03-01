using ContaCerta.Aplication.Users;
using ContaCerta.Aplication.Users.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ContaCerta.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : HomeController
{
    private readonly UserApp _userApp;

    public UsersController(UserApp userApp)
    {
        _userApp = userApp;
    }

    [HttpGet]
    [Route("actives")]
    public IActionResult Index()
    {
        var data = _userApp.ListActives();
        return PrepareResult(data);
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create(UserCreateRequest request)
    {
        var data = _userApp.Create(request);
        return PrepareResult(data);
    }

    [HttpGet]
    [Route("last/costs")]
    public IActionResult GetLastCostsByUser([FromQuery] string email)
    {
        var data = _userApp.GetLastCostsByUser(email);
        return PrepareResult(data);
    }
}
