using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Moon.OData;

namespace Moon.AspNet.OData.Sample.Server.Home
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IEnumerable<Entity> GetEntities(ODataQuery<Entity> odata)
        {
            // do whatever you want with parsed OData query options
            return Enumerable.Empty<Entity>();
        }
    }
}