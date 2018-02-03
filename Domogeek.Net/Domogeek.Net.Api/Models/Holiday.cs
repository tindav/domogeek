using System;

namespace Domogeek.Net.Api.Models
{
    public class Holiday : BaseDateResponse
    {
        public Holiday(DateTimeOffset date) : base(date) { }

        public Holiday(DateTimeOffset date, string holidayName, string specificHoliday = null) : base(date)
        {
            HolidayName = holidayName;
            SpecificHoliday = specificHoliday;
        }

        public string HolidayName { get; set; }
        public string SpecificHoliday { get; set; }
    }
}
