using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MVCCustomRoutingEx.Controllers
{
    public class StudentController : Controller
    {
        public string Index(string count)
        {
            return "Index() Action Method of StudentController";
        }
        public string Details(string id)
        {
            return "Details() Action Method of StudentController";
        }
    }
}