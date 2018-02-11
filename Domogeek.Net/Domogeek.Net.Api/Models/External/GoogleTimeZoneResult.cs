using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Models.External
{
    public class GoogleTimeZoneResult
    {
        public long DstOffset { get; set; }
        public long RawOffset { get; set; }
        public string Status { get; set; }
    }

}
