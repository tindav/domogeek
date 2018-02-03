using System;
using System.Threading.Tasks;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
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
        //[SwaggerResponse(200, typeof(SchoolHoliday))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute]SchoolZone zone, [FromRoute] string value)
        {
            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(await _schoolHolidayHelper.GetSchoolHoliday(date.Value, zone));

            return BadRequest();
        }
    }
}
