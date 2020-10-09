using Newtonsoft.Json;

namespace Domogeek.Net.Api.Models.External
{
    public class GoogleGeocodeResult
    {
        public Result[] Results { get; set; }
        public string Status { get; set; }
    }

    public class Result
    {
        [JsonProperty("address_components")]
        public AddressComponents[] AddressComponents { get; set; }
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
        public Geometry Geometry { get; set; }
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        public string[] Types { get; set; }
    }

    public class Geometry
    {
        public GeoCoordinates Location { get; set; }
        [JsonProperty("location_type")]
        public string LocationType { get; set; }
        public Viewport Viewport { get; set; }
    }

    public class GeoCoordinates
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }
        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }

    public class Viewport
    {
        public GeoCoordinates Northeast { get; set; }
        public GeoCoordinates Southwest { get; set; }
    }

    public class AddressComponents
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
        public string[] Types { get; set; }
    }
}
