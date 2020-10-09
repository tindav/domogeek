using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Domogeek.Net.Api.Models;
using Newtonsoft.Json;

namespace Domogeek.Net.Api.Helpers
{
    public class SaintHelper
    {
        private Saint[] Saints { get; }
        public SaintHelper()
        {
            Saints = JsonConvert.DeserializeObject<Saint[]>(File.ReadAllText(@".\Resources\SaintList.json"));
        }

        public IEnumerable<Saint> GetForDate(int day, int month)
        {
            return Saints.Where(s => s.Day == day && s.Month == month);
        }

        public Saint GetForName(string name)
        {
            return Saints.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
