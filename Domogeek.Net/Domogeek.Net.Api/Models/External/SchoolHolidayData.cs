using Newtonsoft.Json;
using System;
using System.Linq;

namespace Domogeek.Net.Api.Models.External
{
    public static class SchoolHolidayDataExtension
    {
        public static SchoolHoliday Holiday(this SchoolHolidayData[] data, DateTimeOffset date, SchoolZone zone)
        {
            return data.FirstOrDefault(h => IsDateInRange(date, h) && IsForZone(zone, h))?.Holidays;
        }

        private static bool IsForZone(SchoolZone zone, SchoolHolidayData h)
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
    }

    public class SchoolHolidayData
    {
        [JsonProperty("fields")]
        public SchoolHoliday Holidays { get; set; }
    }

    public class SchoolHoliday
    {
        public string Zones { get; set; }
        public string[] ZoneList
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Zones))
                    return Zones.Split(' ').Skip(1).ToArray();
                else
                    return null;
            }
        }
        public string Description { get; set; }
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }
    }
}
