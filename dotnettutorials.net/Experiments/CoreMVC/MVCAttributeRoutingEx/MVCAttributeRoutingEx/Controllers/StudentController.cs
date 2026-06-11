using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCAttributeRoutingEx.Controllers
{
    [Route("Home2")]
    public class StudentController : Controller
    {
        [Route("")]                     //http://localhost:61130/Home2
        [Route("Index")]                //http://localhost:61130/Home2/Index
        public string Index()
        {
            return "Index() Action Method of HomeController";
        }

        [Route("Details/{id?}")]        //http://localhost:61130/Home2/Details
        public string Details(int id)
        {
            return "Details() Action Method of HomeController, ID Value = " + id;
        }


        [Route("~/About")]      //http://localhost:61130/About          In order to ignore the Route Template placed at the Controller level, you need to use / or ~/ at the action method level.
        //[Route("/About")]
        public string About()
        {
            return "About() Action Method of HomeController";
        }
    }
}