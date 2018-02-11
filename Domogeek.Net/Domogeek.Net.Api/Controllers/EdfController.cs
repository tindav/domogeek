using System;
using System.Threading.Tasks;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class EdfController : BaseController
    {
        private readonly EdfHelper _edfHelper;

        public EdfController(EdfHelper edfHelper)
        {
            _edfHelper = edfHelper;
        }

        [HttpGet("~/api/ejpedf/{zone}/{value}")]
        [SwaggerResponse(200, typeof(EdfEjpResponse))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute] EjpEdfZoneEnum zone, [FromRoute] string value)
        {
            if (zone == EjpEdfZoneEnum.Unknown)
                return BadRequest("Invalid Zone, must be nord, sud, ouest or paca");

            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(new EdfEjpResponse(date.Value, await _edfHelper.GetEjpAsync(date.Value, zone)));

            return BadRequest("Invalid date, accepted values: now|tomorrow|yesterday|date(YYYY-MM-DD)");
        }

        [HttpGet("~/api/tempoedf/{value}")]
        [SwaggerResponse(200, typeof(EdfTempoResponse))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(new EdfTempoResponse(date.Value, await _edfHelper.GetTempoAsync(date.Value)));

            return BadRequest("Invalid date, accepted values: now|tomorrow|yesterday|date(YYYY-MM-DD)");
        }
    }
}
