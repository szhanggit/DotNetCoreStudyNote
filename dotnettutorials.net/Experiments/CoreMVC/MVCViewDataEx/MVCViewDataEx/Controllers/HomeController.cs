using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCViewDataEx.Models;

namespace MVCViewDataEx.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Details()
        {
            //String string Data
            ViewData["Title"] = "Student Details Page";
            ViewData["Header"] = "Student Details";
            Student student = new Student()
            {
                StudentId = "STD101",
                Name = "James",
                Branch = "CSE",
                Section = "A",
                Gender = "Male"
            };
            //storing Student Data
            ViewData["Student"] = student;
            return View();
        }
    }
}
