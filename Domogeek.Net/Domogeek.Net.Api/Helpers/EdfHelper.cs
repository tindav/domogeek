using Domogeek.Net.Api.Models.External;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Helpers
{
    public class EdfHelper
    {

        private IMemoryCache Cache { get; }

        private const string CachePrefix = "EdfTempo";

        private string CacheKey(DateTimeOffset date) => $"{CachePrefix}-{date.Date}";

        public EdfHelper(IMemoryCache cache)
        {
            Cache = cache;
        }


        const string tempoUrl = "https://particulier.edf.fr/bin/edf_rc/servlets/ejptemponew?Date_a_remonter={0}&TypeAlerte=TEMPO";

        public async Task<TempoEnum> GetTempoAsync(DateTimeOffset date)
        {
            if (Cache.TryGetValue(CacheKey(date), out TempoEnum tempoValue))
            {
                return tempoValue;
            }

            var tempo = await GetTempoFromEdfAsync(date);
            return Cache.Set(CacheKey(date), tempo.JourJ.Tempo, TimeSpan.FromDays(1));
        }

        private async Task<EdfTempo> GetTempoFromEdfAsync(DateTimeOffset date)
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(string.Format(tempoUrl, date.ToString("yyyy-MM-dd")));
                return JsonConvert.DeserializeObject<EdfTempo>(result);
            }
        }
    }
}
