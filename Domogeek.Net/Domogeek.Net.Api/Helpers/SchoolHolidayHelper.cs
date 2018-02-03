using Domogeek.Net.Api.Controllers;
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

        public async Task<string> GetSchoolHoliday(DateTimeOffset date, SchoolZone zone)
        {
            if (Cache.TryGetValue(CacheKey, out SchoolHolidayData[] schoolHolidayData))
            {
                return schoolHolidayData.FirstOrDefault(h => IsDateInRange(date, h) && IsForZone(zone, h))?.Holidays?.Description;
            }

            var schoolHolidays = await GetSchoolHolidayFromOpenData();
            Cache.Set(CacheKey, schoolHolidays, TimeSpan.FromDays(1));
            return schoolHolidays.FirstOrDefault(h => IsDateInRange(date, h))?.Holidays?.Description;
        }

        private bool IsForZone(SchoolZone zone, SchoolHolidayData h)
        {
            if (h.Holidays.ZoneList.Any())
                return h.Holidays.ZoneList.Any(z => z.Equals(zone.ToString(), StringComparison.OrdinalIgnoreCase));

            return true;
        }

        private static bool IsDateInRange(DateTimeOffset date, SchoolHolidayData h)
        {
            if (h.Holidays.EndDate.HasValue)
            {
                return date.Date >= h.Holidays.StartDate.Value.Date && date.Date <= h.Holidays.EndDate.Value.Date;
            }
            else
                return date.Date == h.Holidays.StartDate.Value.Date;
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
