using Newtonsoft.Json;
using System;
using System.Linq;

namespace Domogeek.Net.Api.Models.External
{
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
