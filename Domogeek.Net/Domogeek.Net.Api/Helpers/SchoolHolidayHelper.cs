using Domogeek.Net.Api.Models;
using Domogeek.Net.Api.Models.External;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Helpers
{
    public class SchoolHolidayHelper
    {

        private IMemoryCache Cache { get; }

        public SchoolHolidayHelper(IMemoryCache cache)
        {
            Cache = cache;
        }

        private const string CacheKey = "SchoolHoliday";

        const string schoolHolidayUrl = "https://data.education.gouv.fr/explore/dataset/fr-en-calendrier-scolaire/download?format=json";

        public async Task<SchoolHoliday> GetSchoolHoliday(DateTimeOffset date, SchoolZone zone)
        {
            if (Cache.TryGetValue(CacheKey, out SchoolHolidayData[] schoolHolidayData))
            {
                return schoolHolidayData.Holiday(date, zone);
            }

            var schoolHolidays = await GetSchoolHolidayFromOpenData();
            Cache.Set(CacheKey, schoolHolidays, TimeSpan.FromDays(1));
            return schoolHolidays.Holiday(date, zone);
        }

        private async Task<SchoolHolidayData[]> GetSchoolHolidayFromOpenData()
        {
            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(schoolHolidayUrl);
                return JsonConvert.DeserializeObject<SchoolHolidayData[]>(result);
            }
        }
    }
}
