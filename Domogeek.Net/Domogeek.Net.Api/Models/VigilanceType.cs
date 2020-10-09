using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domogeek.Net.Api.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VigilanceType
    {
        Unknown = 0,
        Color = 1,
        Risk = 2,
        Flood = 3,
        All = 99
    }
}