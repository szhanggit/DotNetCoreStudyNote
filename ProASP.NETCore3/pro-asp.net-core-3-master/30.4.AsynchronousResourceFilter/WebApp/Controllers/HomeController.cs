using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Filters;

namespace WebApp.Controllers
{
    [HttpsOnly]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new StatusCodeResult(StatusCodes.Status403Forbidden);
        }

        public IActionResult Secure()
        {
            return View("Message","This is the Secure action on the Home controller");
        }
    }
}
