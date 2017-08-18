using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Moon.AspNetCore.Authentication.Basic.Sample.Views.Home
{
    public class HomeController : Controller
    {
        [Authorize, HttpGet("")]
        public IActionResult Index()
            => View();
    }
}