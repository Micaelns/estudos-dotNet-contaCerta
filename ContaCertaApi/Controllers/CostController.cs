using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContaCerta.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CostController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateCost(CreateCost _createCost, FindActiveUserByEmail _findUser, [FromQuery] string title, [FromQuery] string? description, [FromQuery] float value, [FromQuery] string email, [FromQuery] DateTime? paymentDate, [FromQuery] bool active)
        {
            try
            {
                var LoggedUser = _findUser.Execute(email);
                var costs = _createCost.Execute(title, description, value, paymentDate, LoggedUser, active);
                return Ok(costs);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{costId}")]
        public IActionResult UpdateCost(FindCost _findCost, UpdateCosts _updateCost, [FromRoute] int costId, [FromQuery] string? title, [FromQuery] string? description, [FromQuery] float? value, [FromQuery] DateTime? paymentDate, [FromQuery] bool? active)
        {
            try
            {
                var initialCost = _findCost.Execute(costId);
                var cost = _updateCost.Execute(initialCost, title, description, value, paymentDate, active);

                if ( cost == null)
                {
                    return NoContent();
                }

                return Ok(cost);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("{costId}/manage-users")]
        public IActionResult ManageUsersCost(FindCost _findCost, FindActiveUserByEmail _findUser, ManageUsers _manageUsers, [FromRoute] int costId, [FromQuery] string[] emailAdd, [FromQuery] string[] emailRemove)
        {
            try
            {
                var cost = _findCost.Execute(costId);
                var usersToAdd = emailAdd.Select(_findUser.Execute).ToArray();
                var usersToRemove = emailRemove.Select(_findUser.Execute).ToArray();
                _manageUsers.Execute(cost, usersToAdd, usersToRemove);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("last/created")]
        public IActionResult GetLastCostsCreatedByUser(LastCostsCreatedByUser _lastCosts, FindActiveUserByEmail _findUser, [FromQuery] string email)
        {
            try
            {
                var LoggedUser = _findUser.Execute(email);
                var costs = _lastCosts.Execute(LoggedUser);
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
        public IActionResult GetNextCostsCreatedByUser(NextCostsCreatedByUser _nextCosts, FindActiveUserByEmail _findUser, [FromQuery] string email)
        {
            try
            {
                var LoggedUser = _findUser.Execute(email);
                var costs = _nextCosts.Execute(LoggedUser);
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
}
