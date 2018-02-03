using System;
using System.Collections.Generic;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class HolidayController : BaseController
    {
        // GET api/values
        [HttpGet("~/api/holiday/{value}")]
        [SwaggerResponse(200, typeof(string))]
        [SwaggerResponse(400)]
        public IActionResult Get([FromRoute] string value)
        {
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
                    return Ok(Holiday(dateFromEnum.Value));
            }
            if (DateTimeOffset.TryParse(value, out DateTimeOffset date))
            {
                return Ok(Holiday(date));
            }
            return BadRequest();
        }

        private string Holiday(DateTimeOffset date)
        {
            return date.ToString("yyyy-MM-dd");

        }
    }
}
