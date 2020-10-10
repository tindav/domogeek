using System;
using System.Globalization;
using System.Threading;

namespace Domogeek.Net.Api.Models
{
    public class HebrewDateResponse
    {
        public HebrewDateResponse(DateTime date)
        {
            var hebrewCalendar = new HebrewCalendar();
            var year = hebrewCalendar.GetYear(date);
            var month = hebrewCalendar.GetMonth(date);
            var day = hebrewCalendar.GetDayOfMonth(date);

            CultureInfo culture = CultureInfo.CreateSpecificCulture("he-IL");
            culture.DateTimeFormat.Calendar = hebrewCalendar;
            Thread.CurrentThread.CurrentCulture = culture;

            HebrewDate = $"{year.ToString().PadLeft(4, '0')}-{month.ToString().PadLeft(2, '0')}-{day.ToString().PadLeft(2, '0')}";
            WrittenDate = $"{day} {GetHebrewMonthDate(month, year, hebrewCalendar)} {year}";
        }

        public string HebrewDate { get; set; }
        public string WrittenDate { get; set; }

        private string GetHebrewMonthDate(int month, int year, HebrewCalendar hebrewCalendar)
        {
            switch (month)
            {
                case 1:
                    return "Tishri";
                case 2:
                    return "Heshvan";
                case 3:
                    return "Kislev";
                case 4:
                    return "Tevet";
                case 5:
                    return "Shevat";
                case 6:
                    return "Adar" + (hebrewCalendar.IsLeapYear(year) ? " I" : string.Empty);
                case 7:
                    return hebrewCalendar.IsLeapYear(year) ? "Adar II" : "Nissan";
                case 8:
                    return hebrewCalendar.IsLeapYear(year) ? "Nissan" : "Iyar";
                case 9:
                    return hebrewCalendar.IsLeapYear(year) ? "Iyar" : "Sivan";
                case 10:
                    return hebrewCalendar.IsLeapYear(year) ? "Sivan" : "Tammouz";
                case 11:
                    return hebrewCalendar.IsLeapYear(year) ? "Tammouz" : "Av";
                case 12:
                    return hebrewCalendar.IsLeapYear(year) ? "Av" : "Eloul";
                case 13:
                    return "Eloul";
                default:
                    return null;
            }
        }
    }
}
