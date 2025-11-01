using HajorPay.ThriftService.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace HajorPay.ThriftService.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult Success<T>(Response<T> response)
        {
            return response.Data is null ? NoContent() : Ok(response);
        }

        protected IActionResult CreatedResponse<T>(Response<T> response, string actionName, object? routeValues = null)
        {
            return CreatedAtAction(actionName, routeValues, response);
        }

        protected IActionResult NoContentResponse()
        {
            return NoContent();
        }

        protected IActionResult Failure<T>(Response<T> response, int statusCode = StatusCodes.Status400BadRequest)
        {
            return StatusCode(statusCode, response);
        }
    }

}
