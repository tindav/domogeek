using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domogeek.Net.Api.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum EjpEdfZoneEnum
    {
        Unknown = 0,
        Nord = 1,
        Sud = 2,
        Ouest = 3,
        Paca = 4
    }
}
