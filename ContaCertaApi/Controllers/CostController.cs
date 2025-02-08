using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContaCerta.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CostController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateCost(ManagerCost _managerCost, ManagerUser _managerUser, [FromQuery] string title, [FromQuery] string? description, [FromQuery] float value, [FromQuery] string email, [FromQuery] DateTime? paymentDate, [FromQuery] bool active)
    {
        try
        {
            var LoggedUser = _managerUser.FindActiveByEmail(email);
            var costs = _managerCost.Create(title, description, value, paymentDate, LoggedUser, active);
            return Ok(costs);
        } catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    [Route("{costId}/add-users")]
    public IActionResult AddUsersCost(ManagerCost _managerCost, ManagerUser _managerUser, ManagerUsersInCost _addUsers, [FromRoute] int costId, [FromBody] string[] emailUser)
    {
        try
        {
            var cost = _managerCost.Find(costId);
            var users = emailUser.Select(_managerUser.FindActiveByEmail).ToArray();
            _addUsers.AddUsers(users, cost);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("last/created")]
    public IActionResult GetLastCostsCreatedByUser(ManagerCost _lastCosts, ManagerUser _managerUser, [FromQuery] string email)
    {
        try
        {
            var LoggedUser = _managerUser.FindActiveByEmail(email);
            var costs = _lastCosts.LastCostsCreatedByUser(LoggedUser);
            if (costs.Length == 0)
                return NoContent();

            return Ok(costs);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    [Route("next/created")]
    public IActionResult GetNextCostsCreatedByUser(ManagerCost _nextCosts, ManagerUser _managerUser, [FromQuery] string email)
    {
        try
        {
            var LoggedUser = _managerUser.FindActiveByEmail(email);
            var costs = _nextCosts.NextCostsCreatedByUser(LoggedUser);
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
