using System;

namespace Domogeek.Net.Api.Models
{
    public abstract class BaseDateResponse
    {
        public BaseDateResponse(DateTimeOffset date, bool dayOnly = true)
        {
            Date = dayOnly ? date.Date : date;
        }

        public DateTimeOffset Date { get; set; }
    }
}
