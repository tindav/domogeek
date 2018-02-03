using System;
using System.Threading.Tasks;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class TempoEdfController : BaseController
    {
        private readonly EdfHelper _edfHelper;

        public TempoEdfController(EdfHelper edfHelper)
        {
            _edfHelper = edfHelper;
        }

        [HttpGet("~/api/tempoedf/{value}")]
        [SwaggerResponse(200, typeof(EdfTempoResponse))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute] string value)
        {
            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok(new EdfTempoResponse(date.Value, await _edfHelper.GetTempoAsync(date.Value)));

            return BadRequest();
        }
    }
}
