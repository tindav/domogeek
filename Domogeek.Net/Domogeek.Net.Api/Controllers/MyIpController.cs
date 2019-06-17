using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Domogeek.Net.Api.Controllers
{
    public class MyIpController : BaseController
    {
        [HttpGet("~/api/myip")]
        [SwaggerResponse(200, Type = typeof(string))]
        [SwaggerResponse(400)]
        public IActionResult Get()
        {
            return Ok(Request.HttpContext.Connection.RemoteIpAddress.ToString());
        }
    }
}
