using ContaCerta.Domain.Costs.Services;
using ContaCerta.Domain.Users.Model;
using Microsoft.AspNetCore.Mvc;

namespace ContaCerta.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CostController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateCost(CreateCost _createCost ,[FromQuery] string title ,[FromQuery] string? description ,[FromQuery] float value ,[FromQuery] DateTime? paymentDate ,[FromQuery] bool active)
        {
            try
            {
                var costs = _createCost.Execute(title, description, value, paymentDate, new User("user", "****", true), active);
                return Ok(costs);
            } catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
