using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        rouge,
        bleu,
        blanc
    }
}
