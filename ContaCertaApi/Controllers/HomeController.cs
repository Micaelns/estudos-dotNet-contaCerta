using ContaCerta.Aplication.Common;
using Microsoft.AspNetCore.Mvc;
using StatusCodeCommon = ContaCerta.Aplication.Common.StatusCode;

namespace ContaCerta.Api.Controllers;

public class HomeController : ControllerBase
{
    protected IActionResult PrepareResult(Response element)
    {
        return element.Status switch
        {
            StatusCodeCommon.Success => Ok(element),
            StatusCodeCommon.Created => Created(),
            StatusCodeCommon.NoContent => NoContent(),
            _ => BadRequest(element.StatusMessage),
        };
    }
}
