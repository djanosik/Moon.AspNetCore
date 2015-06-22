using System.Linq;
using Microsoft.AspNet.Mvc;

namespace Moon.AspNet.Sample.Pages.Home
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View(new IndexModel
            {
                Items = Enumerable.Range(1, 10)
            });
        }
    }
}