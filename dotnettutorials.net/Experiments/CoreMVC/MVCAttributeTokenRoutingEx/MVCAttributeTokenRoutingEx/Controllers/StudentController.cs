using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCAttributeTokenRoutingEx.Controllers
{
    [Route("[controller]/[action]")]
    public class StudentController : Controller
    {
        public IActionResult Index()                     //http://localhost:61563/Student/Index
        {
            return View();
        }
    }
}