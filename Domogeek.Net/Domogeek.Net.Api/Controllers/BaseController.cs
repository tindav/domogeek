using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domogeek.Net.Api.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController:Controller
    {
    }
}
