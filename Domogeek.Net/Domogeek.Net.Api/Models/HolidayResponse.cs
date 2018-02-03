namespace Domogeek.Net.Api.Models
{
    public class Holiday
    {
        public Holiday() { }

        public Holiday(string holidayName, string specificHoliday = null)
        {
            HolidayName = holidayName;
            SpecificHoliday = specificHoliday;
        }

        public string HolidayName { get; set; }
        public string SpecificHoliday { get; set; }
    }

    public class HolidayResponse : Holiday
    {
        public HolidayResponse(Holiday holiday)
        {
            IsHoliday = holiday != null;
            HolidayName = holiday?.HolidayName;
            SpecificHoliday = holiday?.SpecificHoliday;
        }

        public bool IsHoliday { get; set; }
    }
}
