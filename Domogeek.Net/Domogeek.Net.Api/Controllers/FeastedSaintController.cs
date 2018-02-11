using System;
using System.Net;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class FeastedSaintController : BaseController
    {
        private readonly SaintHelper _saintHelper;

        public FeastedSaintController(SaintHelper saintHelper)
        {
            _saintHelper = saintHelper;
        }

        [HttpGet("~/api/feastedsaint/{value}")]
        [SwaggerResponse(200, typeof(Saint))]
        [SwaggerResponse(200, typeof(Saint[]))]
        [SwaggerResponse(400)]
        public IActionResult Get([FromRoute] string value)
        {
            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(_saintHelper.GetForDate(date.Value.Day, date.Value.Month));

            return Ok(_saintHelper.GetForName(value));
        }
    }
}
