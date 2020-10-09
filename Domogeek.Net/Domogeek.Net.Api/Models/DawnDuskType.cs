using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domogeek.Net.Api.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DawnDuskType
    {
        Unknown,
        Sunrise,
        Sunset,
        Zenith,
        DayDuration,
        All
    }
}