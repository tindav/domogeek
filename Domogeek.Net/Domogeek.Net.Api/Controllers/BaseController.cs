using Domogeek.Net.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController : Controller
    {
        protected static DateTimeOffset? GetDateFromInput(string value)
        {
            DateTimeOffset? date = null;
            if (Enum.TryParse(value, true, out DateEnum enumValue))
            {
                switch (enumValue)
                {
                    case DateEnum.now:
                        date = DateTimeOffset.Now;
                        break;
                    case DateEnum.tomorrow:
                        date = DateTimeOffset.Now.AddDays(1);
                        break;
                    case DateEnum.yesterday:
                        date = DateTimeOffset.Now.AddDays(-1);
                        break;
                }
            }
            if (DateTimeOffset.TryParse(value, out DateTimeOffset dateFromString))
            {
                date = dateFromString;
            }

            return date;
        }
    }

}
