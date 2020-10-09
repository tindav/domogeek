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
        private string BingApiKey { get; }

        private string CacheKey(string searchedLocation) => $"{CachePrefix}-{searchedLocation.ToLowerInvariant()}";

        public GeolocationHelper(IMemoryCache cache, IConfiguration configuration)
        {
            Cache = cache;
            GoogleApiKey = configuration["Geolocation:GoogleApiKey"];
            BingApiKey = configuration["Geolocation:BingApiKey"];
        }

        const string googleUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";
        const string bingUrl = "http://dev.virtualearth.net/REST/v1/Locations/{0}?maxResults=1&key={1}";

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

            var bingPosition = await GetCoordinatesFromBingAsync(location);
            if (bingPosition?.StatusCode == 200)
            {
                var result = (GeoCoordinates)bingPosition.ResourceSets.First().Resources.First().Point.Coordinates;
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

        private async Task<BingGeocodeResult> GetCoordinatesFromBingAsync(string location)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetAsync(string.Format(bingUrl, location, BingApiKey));
                if (result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<BingGeocodeResult>(await result.Content.ReadAsStringAsync());
                else
                    return null;
            }
        }
    }
}
