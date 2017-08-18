using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Moon.AspNetCore.Mvc.Sample.Views.Home
{
    public class HomeController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View(new IndexModel {
                Items = Enumerable.Range(1, 10)
            });
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            throw Response.Error("Something happened!");
        }
    }
}