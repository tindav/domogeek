using System;
using System.Threading.Tasks;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class DawnDuskController : BaseController
    {
        private readonly GeolocationHelper _geolocationHelper;

        public DawnDuskController(GeolocationHelper geoLocationHelper)
        {
            _geolocationHelper = geoLocationHelper;
        }

        [HttpGet("~/api/dawndusk/{location}/{dawnduskType}/{dateInput}")]
        [SwaggerResponse(200, Type = typeof(DawnDusk))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute] string location, DawnDuskType dawnduskType, string dateInput)
        {
            if (dawnduskType == DawnDuskType.Unknown)
                return BadRequest("Invalid Dawn/Dusk request type, accepted values: sunrise|sunset|zenith|dayDuration|all");

            if (string.IsNullOrWhiteSpace(location))
                return BadRequest("Location must be provided");

            DateTimeOffset? date = GetDateFromInput(dateInput);

            if (date.HasValue)
            {
                var coordinates = await _geolocationHelper.GetLocationAsync(location);
                if (coordinates == null)
                    return BadRequest($"Location {location} not found");

                var utcOffset = await _geolocationHelper.GetUtcOffsetAsync(coordinates, date.Value);

                return Ok(new DawnDusk(date.Value, coordinates, dawnduskType, utcOffset));
            }
            return BadRequest("Invalid date, accepted values: now|tomorrow|yesterday|date(YYYY-MM-DD)");
        }
    }
}
