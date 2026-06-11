using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCBasicEx.Models;

namespace MVCBasicEx.Controllers
{
    public class StudentController : Controller
    {
        public ActionResult Details(int studentId)
        {
            StudentBusinessLayer studentBL = new StudentBusinessLayer();
            Student studentDetail = studentBL.GetById(studentId);
            return View(studentDetail);
        }
    }
}