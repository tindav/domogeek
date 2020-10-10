using System.Runtime.Serialization;

namespace Domogeek.Net.Api.Models.External
{
    public class EdfTempo
    {
        public Jour JourJ { get; set; }
        public Jour JourJ1 { get; set; }
    }

    public class Jour
    {
        public TempoEnum Tempo { get; set; }
    }

    public enum TempoEnum
    {
        nd,
        [EnumMember(Value = "TEMPO_ROUGE")]
        rouge,
        [EnumMember(Value = "TEMPO_BLEU")]
        bleu,
        [EnumMember(Value = "TEMPO_BLANC")]
        blanc
    }
}
