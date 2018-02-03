using Domogeek.Net.Api.Models.External;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Domogeek.Net.Api.Models
{
    public class EdfTempoResponse : BaseDateResponse
    {
        public EdfTempoResponse(DateTimeOffset date, TempoEnum tempo) : base(date)
        {
            Tempo = tempo;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public TempoEnum Tempo { get; }
    }
}
