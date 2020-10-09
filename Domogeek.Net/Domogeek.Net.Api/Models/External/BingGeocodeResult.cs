namespace Domogeek.Net.Api.Models.External
{
    public class BingGeocodeResult
    {
        public Resourceset[] ResourceSets { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string TraceId { get; set; }
    }

    public class Resourceset
    {
        public Resource[] Resources { get; set; }
    }

    public class Resource
    {
        public double[] Bbox { get; set; }
        public string Name { get; set; }
        public Geocodepoint Point { get; set; }
        public Address Address { get; set; }
        public string Confidence { get; set; }
        public string EntityType { get; set; }
        public Geocodepoint[] GeocodePoints { get; set; }
        public string[] MatchCodes { get; set; }
    }

    public class Address
    {
        public string AdminDistrict { get; set; }
        public string AdminDistrict2 { get; set; }
        public string CountryRegion { get; set; }
        public string FormattedAddress { get; set; }
        public string Locality { get; set; }
    }

    public class Geocodepoint
    {
        public string Type { get; set; }
        public double[] Coordinates { get; set; }
        public string CalculationMethod { get; set; }
        public string[] UsageTypes { get; set; }
    }
}
