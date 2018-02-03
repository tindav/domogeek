using System;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class WeekendController : BaseController
    {
        [HttpGet("~/api/weekend/{value}")]
        [SwaggerResponse(200, typeof(Weekend))]
        [SwaggerResponse(400)]
        public IActionResult Get([FromRoute] string value, CountryEnum? country)
        {
            if (!country.HasValue)
                country = CountryEnum.fr;

            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(new Weekend(date.Value, country.Value));

            return BadRequest();
        }
    }
}
