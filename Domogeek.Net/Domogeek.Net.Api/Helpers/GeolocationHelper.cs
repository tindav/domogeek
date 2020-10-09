using Domogeek.Net.Api.Models;
using Domogeek.Net.Api.Models.External;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Helpers
{
    public class GeolocationHelper
    {

        private IMemoryCache Cache { get; }

        private const string CachePrefix = "GeolocationHelper";

        private string GoogleApiKey { get; }

        private string CacheKey(string searchedLocation) => $"{CachePrefix}-{searchedLocation.ToLowerInvariant()}";

        public GeolocationHelper(IMemoryCache cache, IConfiguration configuration)
        {
            Cache = cache;
            GoogleApiKey = configuration["Geolocation:GoogleApiKey"];
        }

        const string googleUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";

        public async Task<GeoCoordinates> GetLocationAsync(string location)
        {
            if (Cache.TryGetValue(CacheKey(location), out GeoCoordinates coordinates))
                return coordinates;

            var googlePosition = await GetCoordinatesFromGoogleAsync(location);
            if (googlePosition.Status == "OK")
            {
                var result = googlePosition.Results.First().Geometry.Location;
                return Cache.Set(CacheKey(location), result, TimeSpan.FromDays(1));
            }

            return null;
        }

        private async Task<GoogleGeocodeResult> GetCoordinatesFromGoogleAsync(string location)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(string.Format(googleUrl, location, GoogleApiKey));
                return JsonConvert.DeserializeObject<GoogleGeocodeResult>(result);
            }
        }
    }
}
