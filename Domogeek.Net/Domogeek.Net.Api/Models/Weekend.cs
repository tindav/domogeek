﻿using Domogeek.Net.Api.Models;
using System;

namespace Domogeek.Net.Api.Models
{
    public class Weekend : BaseDateResponse
    {
        public Weekend(DateTimeOffset date, CountryEnum country = CountryEnum.fr) : base(date)
        {
            Country = country;
        }
        public bool IsWeekEnd
        {
            get
            {
                switch (Country)
                {
                    case CountryEnum.fr:
                        return Date.DayOfWeek == DayOfWeek.Saturday || Date.DayOfWeek == DayOfWeek.Sunday;
                    default: return false;
                }
            }
        }

        public CountryEnum Country { get; }
    }
}
