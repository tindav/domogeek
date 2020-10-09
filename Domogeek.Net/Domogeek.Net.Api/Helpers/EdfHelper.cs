using System;
using System.Net.Http;
using System.Threading.Tasks;
using Domogeek.Net.Api.Models;
using Domogeek.Net.Api.Models.External;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Domogeek.Net.Api.Helpers
{
    public class EdfHelper
    {
        private IMemoryCache Cache { get; }
        public IHttpClientFactory HttpClientFactory { get; }

        private const string TempoCachePrefix = "EdfTempo";
        private const string EjpCachePrefix = "EdfEjp";

        private string TempoCacheKey(DateTimeOffset date) => $"{TempoCachePrefix}-{date.Date}";
        private string EjpCacheKey(DateTimeOffset date) => $"{EjpCachePrefix}-{date.Date}";

        public EdfHelper(IMemoryCache cache, IHttpClientFactory httpClientFactory)
        {
            Cache = cache;
            HttpClientFactory = httpClientFactory;
        }

        const string ejpUrl = "https://particulier.edf.fr/bin/edf_rc/servlets/ejptemponew?Date_a_remonter={0}&TypeAlerte=EJP";
        const string tempoUrl = "https://particulier.edf.fr/bin/edf_rc/servlets/ejptemponew?Date_a_remonter={0}&TypeAlerte=TEMPO";

        public async Task<TempoEnum> GetTempoAsync(DateTimeOffset date)
        {
            if (Cache.TryGetValue(TempoCacheKey(date), out TempoEnum tempoValue))
            {
                return tempoValue;
            }

            var tempo = await GetTempoFromEdfAsync(date);
            return Cache.Set(TempoCacheKey(date), tempo.JourJ.Tempo, TimeSpan.FromDays(1));
        }

        public async Task<bool?> GetEjpAsync(DateTimeOffset date, EjpEdfZoneEnum zone)
        {
            if (Cache.TryGetValue(EjpCacheKey(date), out EdfEjp ejpValue))
            {
                return ejpValue.EjpZone(zone);
            }

            var ejp = await GetEjpFromEdfAsync(date);
            Cache.Set(TempoCacheKey(date), ejp, TimeSpan.FromDays(1));
            return ejp.EjpZone(zone);
        }

        private async Task<EdfTempo> GetTempoFromEdfAsync(DateTimeOffset date)
        {
            var client = HttpClientFactory.CreateClient();
            var result = await client.GetStringWithAcceptAndKeepAliveAsync(string.Format(tempoUrl, date.ToString("yyyy-MM-dd")));
            return JsonConvert.DeserializeObject<EdfTempo>(result);
        }

        private async Task<EdfEjp> GetEjpFromEdfAsync(DateTimeOffset date)
        {
            var client = HttpClientFactory.CreateClient();
            var result = await client.GetStringWithAcceptAndKeepAliveAsync(string.Format(ejpUrl, date.ToString("yyyy-MM-dd")));
            return JsonConvert.DeserializeObject<EdfEjp>(result);
        }
    }
}
