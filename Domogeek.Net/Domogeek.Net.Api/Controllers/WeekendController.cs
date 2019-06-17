using System;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Domogeek.Net.Api.Controllers
{
    public class WeekendController : BaseController
    {
        [HttpGet("~/api/weekend/{value}")]
        [SwaggerResponse(200, Type = typeof(Weekend))]
        [SwaggerResponse(400)]
        public IActionResult Get([FromRoute] string value, CountryEnum? country)
        {
            if (!country.HasValue)
                country = CountryEnum.fr;

            if (country == CountryEnum.unknown)
                return BadRequest("Invalid Country");

            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(new Weekend(date.Value, country.Value));

            return BadRequest("Invalid date, accepted values: now|tomorrow|yesterday|date(YYYY-MM-DD)");
        }
    }
}
