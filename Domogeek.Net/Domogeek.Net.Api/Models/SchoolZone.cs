using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domogeek.Net.Api.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SchoolZone
    {
        Unknown,
        A,
        B,
        C
    }
}