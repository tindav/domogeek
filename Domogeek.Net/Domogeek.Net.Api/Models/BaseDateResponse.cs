using System;

namespace Domogeek.Net.Api.Models
{
    public abstract class BaseDateResponse
    {
        public BaseDateResponse(DateTimeOffset date)
        {
            Date = date.Date;
        }

        public DateTimeOffset Date { get; set; }
    }
}
