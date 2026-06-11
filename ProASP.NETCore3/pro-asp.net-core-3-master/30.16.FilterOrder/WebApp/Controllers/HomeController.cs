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
    /*  https://localhost:44350 */
    [Message("This is the controller-scoped filter")]
    public class HomeController : Controller
    {
        [Message("This is the first action-scoped filter")]
        [Message("This is the second action-scoped filter")]
        public IActionResult Index()
        {
            return View("Message", "This is the Index action on the Home controller");
        }
    }
}
