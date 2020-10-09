using System;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Domogeek.Net.Api.Controllers
{
    public class HolidayController : BaseController
    {
        [HttpGet("~/api/holiday/{value}")]
        [SwaggerResponse(200, Type = typeof(HolidayResponse))]
        [SwaggerResponse(400)]
        public IActionResult Get(
            [FromRoute] string value,
            [FromRoute] CountryEnum? country)
        {
            if (!country.HasValue)
                country = CountryEnum.fr;

            if (country == CountryEnum.unknown)
                return BadRequest("Invalid Country");

            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(new HolidayResponse(date.Value, HolidayHelper.GetHoliday(date.Value, country.Value)));

            return BadRequest("Invalid date, accepted values: now|tomorrow|yesterday|date(YYYY-MM-DD)");
        }
    }
}
