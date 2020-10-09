using System;

namespace Domogeek.Net.Api.Models
{
    public class HolidayResponse : Holiday
    {
        public HolidayResponse(DateTimeOffset date) : base(date)
        {
        }

        public HolidayResponse(DateTimeOffset date, Holiday holiday) : base(date, holiday?.HolidayName, holiday?.SpecificHoliday)
        {
        }

        public bool IsHoliday => !string.IsNullOrWhiteSpace(HolidayName);
    }
}
