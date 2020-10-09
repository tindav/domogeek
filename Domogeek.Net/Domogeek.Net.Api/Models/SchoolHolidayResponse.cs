using Domogeek.Net.Api.Models.External;
using System;

namespace Domogeek.Net.Api.Models
{
    public class SchoolHolidayResponse : BaseDateResponse
    {
        public SchoolHolidayResponse(DateTimeOffset date) : base(date)
        {
        }

        public SchoolHolidayResponse(DateTimeOffset date, SchoolHoliday holiday) : base(date)
        {
            ZoneList = holiday?.ZoneList;
            Description = holiday?.Description;
        }

        public string[] ZoneList { get; set; }

        public string Description { get; set; }

        public bool IsSchoolHoliday => !string.IsNullOrWhiteSpace(Description);
    }
}
