using System;

namespace Domogeek.Net.Api.Models
{
    public class EdfEjpResponse : BaseDateResponse
    {
        public EdfEjpResponse(DateTimeOffset date, bool? ejp) : base(date)
        {
            Ejp = ejp;
        }

        public bool? Ejp { get; }
    }
}
