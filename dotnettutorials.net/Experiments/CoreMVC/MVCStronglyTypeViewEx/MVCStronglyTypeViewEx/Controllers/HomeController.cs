using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCStronglyTypeViewEx.Models;

namespace MVCStronglyTypeViewEx.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Details()
        {
            ViewBag.Title = "Student Details Page";
            ViewBag.Header = "Student Details";
            Student student = new Student()
            {
                StudentId = "101",
                Name = "James",
                Branch = "CSE",
                Section = "A",
                Gender = "Male"
            };

            return View(student);
        }
    }
}
