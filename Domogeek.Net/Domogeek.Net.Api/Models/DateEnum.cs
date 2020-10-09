using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domogeek.Net.Api.Models
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DateEnum
    {
        now = 0,
        tomorrow = 1,
        yesterday = 2,
        //all = 100
    }
}
