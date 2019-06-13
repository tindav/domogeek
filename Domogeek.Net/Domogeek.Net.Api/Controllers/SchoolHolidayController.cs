using System;
using System.Threading.Tasks;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class SchoolHolidayController : BaseController
    {
        private readonly SchoolHolidayHelper _schoolHolidayHelper;

        public SchoolHolidayController(SchoolHolidayHelper schoolHolidayHelper)
        {
            _schoolHolidayHelper = schoolHolidayHelper;
        }

        [HttpGet("~/api/schoolholiday/{zone}/{value}")]
        [SwaggerResponse(200, Type = typeof(SchoolHolidayResponse))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute]SchoolZone zone, [FromRoute] string value)
        {
            if (zone == SchoolZone.Unknown)
                return BadRequest("Invalid Zone, must be A, B or C");

            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(new SchoolHolidayResponse(date.Value, await _schoolHolidayHelper.GetSchoolHoliday(date.Value, zone)));

            return BadRequest("Invalid date, accepted values: now|tomorrow|yesterday|date(YYYY-MM-DD)");
        }
    }
}
