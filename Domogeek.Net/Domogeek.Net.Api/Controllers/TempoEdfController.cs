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
        [SwaggerResponse(200, typeof(Weekend))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute] string value, CountryEnum? country)
        {
            if (!country.HasValue)
                country = CountryEnum.fr;

            DateTimeOffset? date = GetDateFromInput(value);

            if (date.HasValue)
                return Ok((await _edfHelper.GetTempoAsync(date.Value)).ToString());

            return BadRequest();
        }
    }
}
