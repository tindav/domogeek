using System;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class WeekendController : BaseController
    {
        // GET api/values
        [HttpGet("~/api/weekend/{value}")]
        [SwaggerResponse(200, typeof(bool))]
        [SwaggerResponse(400)]
        public IActionResult Get([FromRoute] string value)
        {
            DateTimeOffset? date = null;
            if (Enum.TryParse(value, true, out DateEnum enumValue))
            {
                switch (enumValue)
                {
                    case DateEnum.now:
                        date = DateTimeOffset.Now;
                        break;
                    case DateEnum.tomorrow:
                        date = DateTimeOffset.Now.AddDays(1);
                        break;
                    case DateEnum.yesterday:
                        date = DateTimeOffset.Now.AddDays(-1);
                        break;
                }
            }
            if (DateTimeOffset.TryParse(value, out DateTimeOffset dateFromString))
            {
                date = dateFromString;
            }

            if (date.HasValue)
                return Ok(new Weekend(date.Value));

            return BadRequest();
        }
    }
}
