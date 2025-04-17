using ContaCerta.Aplication.Costs;
using ContaCerta.Aplication.Costs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace ContaCerta.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CostController : HomeController
{
    private readonly CostApp _costApp;

    public CostController(CostApp costApp)
    {
        _costApp = costApp;
    }

    [HttpPost]
    public IActionResult CreateCost(CostCreateRequest request)
    {
        var data = _costApp.Create(request);
        return PrepareResult(data);
    }

    [HttpPost]
    [Route("{costId}/users")]
    public IActionResult UsersInCost([FromRoute] int costId, [FromBody] UsersInCostRequest emails)
    {
        var data = _costApp.UsersInCost(costId, emails);
        return PrepareResult(data);
    }

    [HttpGet]
    [Route("last")]
    public IActionResult GetLastCostsByUser([FromQuery] string email)
    {
        var data = _costApp.GetLastCostsByUser(email);
        return PrepareResult(data);
    }

    [HttpGet]
    [Route("next")]
    public IActionResult GetNextCostsByUser([FromQuery] string email)
    {
        var data = _costApp.GetNextCostsByUser(email);
        return PrepareResult(data);
    }

}
