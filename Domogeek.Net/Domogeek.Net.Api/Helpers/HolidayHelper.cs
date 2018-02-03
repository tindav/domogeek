using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Helpers
{
    public static class HolidayHelper
    {
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

        public static bool isEaster(DateTimeOffset date)
        {
            return date.Equals(EasterDate(date.Year));
        }

    }
}
