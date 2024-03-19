using ContaCerta.Domain.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContaCerta.Api.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        [Route("actives")]
        public IActionResult Index(ListActivesUsers _listActivesUsers)
        { 
            return Ok(_listActivesUsers.Execute());
        }

        [HttpGet]
        [Route("last/costs")]
        public IActionResult GetLastCostsByUser(FindActiveUserByEmail _findUser, LastCosts _lastCosts, [FromQuery] string email)
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
    }
}
