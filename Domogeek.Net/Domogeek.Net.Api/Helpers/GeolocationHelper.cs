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
        public IHttpClientFactory HttpClientFactory { get; }

        private const string CachePrefix = "GeolocationHelper";

        private string GoogleApiKey { get; }
        private string BingApiKey { get; }

        private string CacheKey(string searchedLocation) => $"{CachePrefix}-{searchedLocation.ToLowerInvariant()}";

        private string CacheKey(GeoCoordinates coordinates, DateTimeOffset date) => $"{CachePrefix}-{coordinates.Latitude},{coordinates.Longitude}-{date.Date.ToString("o")}";

        public GeolocationHelper(IMemoryCache cache, IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            Cache = cache;
            HttpClientFactory = httpClientFactory;
            GoogleApiKey = configuration["Geolocation:GoogleApiKey"];
            BingApiKey = configuration["Geolocation:BingApiKey"];
        }

        const string googleUrl = "https://maps.googleapis.com/maps/api/geocode/json?address={0}&key={1}";
        const string googleTzUrl = "https://maps.googleapis.com/maps/api/timezone/json?location={0},{1}&timestamp={2}&key={3}";
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
                var bingResult = bingPosition.ResourceSets?.FirstOrDefault()?.Resources?.FirstOrDefault();
                if (bingResult != null)
                {
                    var result = (GeoCoordinates)bingResult.Point.Coordinates;
                    return Cache.Set(CacheKey(location), result, TimeSpan.FromDays(1));
                }
            }
            return null;
        }

        public async Task<double> GetUtcOffsetAsync(GeoCoordinates coordinates, DateTimeOffset date)
        {
            date = new DateTimeOffset(new DateTime(date.Year, date.Month, date.Day), TimeSpan.FromHours(0));

            if (Cache.TryGetValue(CacheKey(coordinates, date), out double offset))
                return offset;

            var googleTimeZoneInfo = await GetTimeZoneFromGoogleAsync(coordinates, date.Date);
            if (googleTimeZoneInfo.Status == "OK")
            {
                var result = (googleTimeZoneInfo.RawOffset + googleTimeZoneInfo.DstOffset) / 3600.0;
                return Cache.Set(CacheKey(coordinates, date), result, TimeSpan.FromDays(1));
            }

            return 0.0;
        }

        private async Task<GoogleGeocodeResult> GetCoordinatesFromGoogleAsync(string location)
        {
            var client = HttpClientFactory.CreateClient();
            var result = await client.GetStringAsync(string.Format(googleUrl, location, GoogleApiKey));
                return JsonConvert.DeserializeObject<GoogleGeocodeResult>(result);
        }

        private async Task<GoogleTimeZoneResult> GetTimeZoneFromGoogleAsync(GeoCoordinates coordinates, DateTime date)
        {
            var client = HttpClientFactory.CreateClient();
            var result = await client.GetAsync(string.Format(googleTzUrl,
                                                                 coordinates.Latitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                                                                 coordinates.Longitude.ToString(System.Globalization.CultureInfo.InvariantCulture),
                                                                 date.ToTimestamp(),
                                                                 GoogleApiKey));
                if (result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<GoogleTimeZoneResult>(await result.Content.ReadAsStringAsync());
                else
                    return null;

        }

        private async Task<BingGeocodeResult> GetCoordinatesFromBingAsync(string location)
        {
            var client = HttpClientFactory.CreateClient();
            var result = await client.GetAsync(string.Format(bingUrl, location, BingApiKey));
                if (result.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<BingGeocodeResult>(await result.Content.ReadAsStringAsync());
                else
                    return null;
        }
    }

    public static class DateTimeExtension
    {
        private static readonly long DatetimeMinTimeTicks =
           (new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Ticks;

        public static long ToTimestamp(this DateTime dt)
        {
            return (long)((dt.ToUniversalTime().Ticks - DatetimeMinTimeTicks) / 10000000);
        }
    }

}
