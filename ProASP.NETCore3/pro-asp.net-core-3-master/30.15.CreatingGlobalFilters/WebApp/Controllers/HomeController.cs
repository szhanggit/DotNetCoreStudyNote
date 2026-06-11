using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApp.Controllers
{
    /*  http://localhost:5000 */
    //[HttpsOnly]
    [ResultDiagnostics]
    //[GuidResponse]
    //[GuidResponse]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }
    }
}
