using System;
using System.Threading.Tasks;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class GeolocationController : BaseController
    {
        private readonly GeolocationHelper _geolocationHelper;

        public GeolocationController(GeolocationHelper geoLocationHelper)
        {
            _geolocationHelper = geoLocationHelper;
        }

        [HttpGet("~/api/geolocation/{location}")]
        [SwaggerResponse(200, typeof(GeoCoordinatesResponse))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute] string location)
        {
            if(!string.IsNullOrWhiteSpace(location))
                return Ok((GeoCoordinatesResponse) await _geolocationHelper.GetLocationAsync(location));

            return BadRequest("location must be provided");
        }
    }
}
