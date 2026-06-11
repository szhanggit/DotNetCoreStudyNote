using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (Request.IsHttps)
            {
                return View("Message","This is the Index action on the Home controller");
            }
            else
            {
                return new StatusCodeResult(StatusCodes.Status403Forbidden);
            }
        }

        [RequireHttps]
        public IActionResult Secure()
        {
            return View("Message","This is the Secure action on the Home controller");
        }
    }
}
