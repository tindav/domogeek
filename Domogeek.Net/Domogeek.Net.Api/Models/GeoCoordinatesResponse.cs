using Domogeek.Net.Api.Models.External;

namespace Domogeek.Net.Api.Models
{
    public class GeoCoordinatesResponse
    {
        public static implicit operator GeoCoordinatesResponse(GeoCoordinates gc)
        {
            if (gc == null)
                return null;

            return new GeoCoordinatesResponse
            {
                Longitude = gc.Longitude,
                Latitude = gc.Latitude
            };
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
