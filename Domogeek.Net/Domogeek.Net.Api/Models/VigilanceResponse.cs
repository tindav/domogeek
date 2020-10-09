using Domogeek.Net.Api.Models.External;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Domogeek.Net.Api.Models
{
    public class VigilanceResponse
    {
        public VigilanceResponse(DataVigilance data, VigilanceType vigilanceType)
        {
            if (vigilanceType == VigilanceType.All || vigilanceType == VigilanceType.Color)
                Couleur = data.CouleurValue;
            if (vigilanceType == VigilanceType.All || vigilanceType == VigilanceType.Risk)
                Risque = data.Risque?.RisqueValeur;
            if (vigilanceType == VigilanceType.All || vigilanceType == VigilanceType.Flood)
                Crue = data.Crue?.CrueValeur;
            Departement = data.Departement;
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public CouleurEnum? Couleur { get; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RisqueEnum? Risque { get; }
        [JsonConverter(typeof(StringEnumConverter))]
        public CouleurEnum? Crue { get; }
        public string Departement { get; }
    }
}
