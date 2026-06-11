using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private DataContext context;

        public HomeController(DataContext ctx)
        {
            context = ctx;
        }

        public async Task<IActionResult> Index(long id = 1)
        {
            //return View(await context.Products.FindAsync(id));
            Product prod = await context.Products.FindAsync(id);
            if (prod.CategoryId == 1)
            {
                return View("Watersports", prod);
            }
            else
            {
                return View(prod);
            }
        }

        /*
            If you need to define a method in a controller that is not an action, you can make it private or,
            if that is not possible, decorate the method with the NonAction attribute.
        */

        public IActionResult Common()
        {
            //return View("/Views/Shared/Common.cshtml");
            return View();
        }
    }
}
