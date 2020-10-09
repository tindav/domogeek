using System;
using System.Collections.Generic;
using System.Linq;
using Domogeek.Net.Api.Models;

namespace Domogeek.Net.Api.Helpers
{
    public static class HolidayHelper
    {
        public static List<Holiday> Holidays(int year, CountryEnum country)
        {

            var holidays = new List<Holiday>();

            var ed = EasterDate(year);

            switch (country)
            {
                case CountryEnum.fr:
                    holidays.Add(new Holiday(new DateTime(year, 1, 1), "Jour de l'an"));
                    holidays.Add(new Holiday(ed.AddDays(-2), "Vendredi Saint", "Alsace-Moselle"));
                    holidays.Add(new Holiday(ed, "Dimanche de Pâques"));
                    holidays.Add(new Holiday(ed.AddDays(1), "Lundi de Pâques"));
                    holidays.Add(new Holiday(new DateTime(year, 5, 1), "Fête du travail"));
                    holidays.Add(new Holiday(new DateTime(year, 5, 8), "Victoire des alliés 1945"));
                    holidays.Add(new Holiday(ed.AddDays(39), "Jeudi de l'ascension"));
                    holidays.Add(new Holiday(ed.AddDays(49), "Dimanche de Pentecôte"));
                    holidays.Add(new Holiday(ed.AddDays(50), "Lundi de Pentecôte"));
                    holidays.Add(new Holiday(new DateTime(year, 7, 14), "Fête Nationale"));
                    holidays.Add(new Holiday(new DateTime(year, 8, 15), "Assomption"));
                    holidays.Add(new Holiday(new DateTime(year, 11, 1), "Toussaint"));
                    holidays.Add(new Holiday(new DateTime(year, 11, 11), "Armistice 1918"));
                    holidays.Add(new Holiday(new DateTime(year, 12, 25), "Jour de Noël"));
                    holidays.Add(new Holiday(new DateTime(year, 12, 26), "Saint Etienne", "Alsace"));
                    break;
            }

            return holidays;
        }

        public static DateTimeOffset EasterDate(int year)
        {
            //Detarminate easter date
            int day = 0;
            int month = 0;

            int yearMod19 = year % 19;
            int century = year / 100;
            int h = (century - (int)(century / 4) - (int)((8 * century + 13) / 25) + 19 * yearMod19 + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - yearMod19) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - century + (int)(century / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTimeOffset(new DateTime(year, month, day));
        }

        public static Holiday GetHoliday(DateTimeOffset date, CountryEnum country)
        {
            return Holidays(date.Year, country).FirstOrDefault(h => h.Date.Equals(date));
        }
    }
}
