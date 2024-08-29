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
                var cost = _findCost.Execute(costId);
                cost = _updateCost.Execute(cost, title, description, value, paymentDate, active);
                return Ok(cost);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("{costId}/add-users")]
        public IActionResult AddUsersCost(FindCost _findCost, FindActiveUserByEmail _findUser, AddUsers _addUsers, [FromRoute] int costId, [FromBody] string[] emailUser)
        {
            try
            {
                var cost = _findCost.Execute(costId);
                var users = emailUser.Select(_findUser.Execute).ToArray();
                _addUsers.Execute(cost, users);
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
