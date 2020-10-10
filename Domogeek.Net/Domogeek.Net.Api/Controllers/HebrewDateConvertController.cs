using System;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Domogeek.Net.Api.Controllers
{
    public class HebrewDateConvertController : BaseController
    {
        [HttpGet("~/api/to/hebrew/from/{value}")]
        [SwaggerResponse(200, Type = typeof(Weekend))]
        [SwaggerResponse(400)]
        public IActionResult Get(
            [FromRoute] string value)
        {
            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
            {
                return Ok(new HebrewDateResponse(date.Value.Date));
            }

            return BadRequest("Invalid date, accepted values: now|tomorrow|yesterday|date(YYYY-MM-DD)");
        }
    }
}
