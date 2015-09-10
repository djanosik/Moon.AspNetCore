using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Moon.AspNet.Sample.Pages.Home
{
    public class HomeController : Controller
    {
        [Authorize, HttpGet("")]
        public IActionResult Index()
            => View();
    }
}