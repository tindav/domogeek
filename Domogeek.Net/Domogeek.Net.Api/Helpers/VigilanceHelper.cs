using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domogeek.Net.Api.Models;
using Domogeek.Net.Api.Models.External;
using Microsoft.Extensions.Caching.Memory;

namespace Domogeek.Net.Api.Helpers
{
    public class VigilanceHelper
    {
        private IMemoryCache Cache { get; }
        public IHttpClientFactory HttpClientFactory { get; }

        private const string CacheKey = "Vigilance";

        public VigilanceHelper(IMemoryCache cache, IHttpClientFactory httpClientFactory)
        {
            Cache = cache;
            HttpClientFactory = httpClientFactory;
        }

        const string vigilanceUrl = "http://vigilance.meteofrance.com/data/NXFR34_LFPW_.xml";

        public async Task<DataVigilance> GetVigilanceAsync(string departement, VigilanceType vigilanceType)
        {
            if (Cache.TryGetValue(CacheKey, out CarteVigilance vigilanceValue))
            {
                return vigilanceValue.Data.FirstOrDefault(v => v.Departement == departement);
            }

            var vigilance = await GetVigilanceFromMeteoFranceAsync();
            Cache.Set(CacheKey, vigilance, TimeSpan.FromDays(1));
            return vigilance.Data.FirstOrDefault(v => v.Departement == departement); ;
        }

        private async Task<CarteVigilance> GetVigilanceFromMeteoFranceAsync()
        {
            var client = HttpClientFactory.CreateClient();
            var result = await client.GetStringAsync(vigilanceUrl);
            return XmlHelper.XmlDeserialize<CarteVigilance>(result);
        }
    }
}
