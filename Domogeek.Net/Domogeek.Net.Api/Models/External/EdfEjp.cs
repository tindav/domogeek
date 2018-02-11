using System;
using System.Collections.Generic;
using System.Linq;

namespace Domogeek.Net.Api.Models.External
{
    public static class EjpEdfExtension
    {
        public static bool? EjpZone(this EdfEjp data, EjpEdfZoneEnum zone)
        {
            var ejp = data.JourJ.FirstOrDefault(z => z.Key.Equals($"ejp{zone.ToString()}",
                                                                StringComparison.OrdinalIgnoreCase)).Value;

            return ejp != null ? (bool?)ejp.Equals("EST_EJP", StringComparison.OrdinalIgnoreCase) : null;
        }
    }

    public class EdfEjp
    {
        public Dictionary<string, string> JourJ { get; set; }
        public Dictionary<string, string> JourJ1 { get; set; }
    }
}
