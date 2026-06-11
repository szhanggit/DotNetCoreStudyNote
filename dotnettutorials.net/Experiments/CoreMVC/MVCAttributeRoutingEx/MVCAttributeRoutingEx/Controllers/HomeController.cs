using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCAttributeRoutingEx.Models;

namespace MVCAttributeRoutingEx.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Route("")]             //http://localhost:61130/
        [Route("Home")]         //http://localhost:61130/Home
        [Route("Home/Index")]   //http://localhost:61130/Home/Index
        public string Index()
        {
            return "Index() Action Method of HomeController";
        }

        [Route("Home/Details/{id?}")]        //http://localhost:61130/Home/Details/100
        public string Details(int id)
        {
            return "Details() Action Method of HomeController, ID Value = " + id;
        }

        [Route("MyHome")]        ////http://localhost:61130/MyHome   
        [Route("MyHome/Index")]  //http://localhost:61130/MyHome/Index
        public string StartPage()
        {
            return "StartPage() Action Method of HomeController";
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
