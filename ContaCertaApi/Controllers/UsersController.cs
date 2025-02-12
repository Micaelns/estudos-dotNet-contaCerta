using ContaCerta.Domain.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContaCerta.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet]
    [Route("actives")]
    public IActionResult Index(ManagerUser _managerUser)
    { 
        return Ok(_managerUser.ListActives());
    }

    [HttpPost]
    [Route("")]
    public IActionResult Create(ManagerUser _managerUser, [FromQuery] string email, [FromQuery] string password)
    {
        try
        {
            var data = _managerUser.Create(email, password);
            return Ok(data);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("last/costs")]
    public IActionResult GetLastCostsByUser(ManagerUser _managerUser, ListCostsUser _lastCosts, [FromQuery] string email)
    {   
        try
        {
            var LoggedUser = _managerUser.FindActiveByEmail(email);
            var costs = _lastCosts.ListCostsLastDays(LoggedUser);
            if (costs.Length == 0)
                return NoContent();

            return Ok(costs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
