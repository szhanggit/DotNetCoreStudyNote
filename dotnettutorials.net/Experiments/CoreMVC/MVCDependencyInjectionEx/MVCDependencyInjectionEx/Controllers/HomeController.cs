using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVCDependencyInjectionEx.Models;

namespace MVCDependencyInjectionEx.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _repository = null;
        //Initialize the variable through constructor
        public HomeController(IStudentRepository repository)
        {
            _repository = repository;
        }
        public JsonResult Index()
        {
            List<Student> allStudentDetails = _repository.GetAllStudent();
            return Json(allStudentDetails);
        }
        public JsonResult GetStudentDetails(int Id)
        {
            Student studentDetails = _repository.GetStudentById(Id);
            return Json(studentDetails);
        }
    }
}
