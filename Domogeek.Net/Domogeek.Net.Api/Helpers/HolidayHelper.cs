using Domogeek.Net.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Helpers
{
    public static class HolidayHelper
    {

        public static Dictionary<DateTimeOffset, Holiday> Holidays(int year, CountryEnum country)
        {

            var holidays = new Dictionary<DateTimeOffset, Holiday>();

            var ed = EasterDate(year);

            switch (country)
            {
                case CountryEnum.fr:
                    holidays.Add(new DateTime(year, 1, 1), new Holiday("Jour de l'an"));
                    holidays.Add(ed.AddDays(-2), new Holiday("Vendredi Saint", "Alsace-Moselle"));
                    holidays.Add(ed, new Holiday("Dimanche de Pâques"));
                    holidays.Add(ed.AddDays(1), new Holiday("Lundi de Pâques"));
                    holidays.Add(new DateTime(year, 5, 1), new Holiday("Fête du travail"));
                    holidays.Add(new DateTime(year, 5, 8), new Holiday("Victoire des alliés 1945"));
                    holidays.Add(ed.AddDays(39), new Holiday("Jeudi de l'ascension"));
                    holidays.Add(ed.AddDays(49), new Holiday("Dimanche de Pentecôte"));
                    holidays.Add(ed.AddDays(50), new Holiday("Lundi de Pentecôte"));
                    holidays.Add(new DateTime(year, 7, 14), new Holiday("Fête Nationale"));
                    holidays.Add(new DateTime(year, 8, 15), new Holiday("Assomption"));
                    holidays.Add(new DateTime(year, 11, 1), new Holiday("Toussaint"));
                    holidays.Add(new DateTime(year, 11, 11), new Holiday("Armistice 1918"));
                    holidays.Add(new DateTime(year, 12, 25), new Holiday("Jour de Noël"));
                    holidays.Add(new DateTime(year, 12, 26), new Holiday("Saint Etienne", "Alsace"));
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
            var holidays = Holidays(date.Year, country);
            return holidays.ContainsKey(date) ? holidays[date] : null;
        }
    }
}
