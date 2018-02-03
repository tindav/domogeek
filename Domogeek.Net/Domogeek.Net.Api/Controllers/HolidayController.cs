using System;
using System.Collections.Generic;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class HolidayController : BaseController
    {
        // GET api/values
        [HttpGet("~/api/holiday/{value}")]
        [SwaggerResponse(200, typeof(HolidayResponse))]
        [SwaggerResponse(400)]
        public IActionResult Get([FromRoute] string value, CountryEnum? country)
        {
            if (!country.HasValue)
                country = CountryEnum.fr;

            if (Enum.TryParse(value, true, out DateEnum enumValue))
            {
                DateTimeOffset? dateFromEnum = null;
                switch (enumValue)
                {
                    case DateEnum.now:
                        dateFromEnum = DateTimeOffset.Now;
                        break;
                    case DateEnum.tomorrow:
                        dateFromEnum = DateTimeOffset.Now.AddDays(1);
                        break;
                    case DateEnum.yesterday:
                        dateFromEnum = DateTimeOffset.Now.AddDays(-1);
                        break;
                }
                if (dateFromEnum.HasValue)
                    return Ok(Holiday(dateFromEnum.Value, country.Value));
            }
            if (DateTimeOffset.TryParse(value, out DateTimeOffset date))
            {
                return Ok(Holiday(date, country.Value));
            }
            return BadRequest();
        }

        private HolidayResponse Holiday(DateTimeOffset date, CountryEnum country)
        {
            var holiday = HolidayHelper.GetHoliday(date, country);
            return new HolidayResponse(date, holiday);
        }
    }
}
