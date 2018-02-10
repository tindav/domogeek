using System;
using System.Threading.Tasks;
using Domogeek.Net.Api.Helpers;
using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domogeek.Net.Api.Controllers
{
    public class VigilanceController : BaseController
    {
        private readonly VigilanceHelper _vigilanceHelper;

        public VigilanceController(VigilanceHelper vigilanceHelper)
        {
            _vigilanceHelper = vigilanceHelper;
        }

        [HttpGet("~/api/vigilance/{departement}/{vigilanceType}")]
        [SwaggerResponse(200, typeof(EdfTempoResponse))]
        [SwaggerResponse(400)]
        public async Task<IActionResult> Get([FromRoute] string departement, [FromRoute] VigilanceType vigilanceType)
        {
            if (vigilanceType == VigilanceType.Unknown)
                return BadRequest("Invalid vigilance request. Accepted values: color|risk|flood|all");

            if (string.IsNullOrWhiteSpace(departement))
                return BadRequest("departement must be provided");

            if (departement.Length < 2)
                departement = departement.PadLeft(2, '0');

            if (departement == "92" || departement == "93" || departement == "94")
                departement = "75";
            if (departement == "20")
                departement = "2A";

            var response = await _vigilanceHelper.GetVigilanceAsync(departement, vigilanceType);
            if (response != null)
                return Ok(new VigilanceResponse(response, vigilanceType));
            return BadRequest($"Department {departement} not found");
        }
    }
}
