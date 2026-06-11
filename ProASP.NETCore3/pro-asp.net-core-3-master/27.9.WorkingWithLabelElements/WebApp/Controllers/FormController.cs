using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class FormController : Controller
    {
        private DataContext context;

        public FormController(DataContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index(long id = 1)
        {
            return View("Form", await context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstAsync(p => p.ProductId == id));
        }

        [HttpPost]
        public IActionResult SubmitForm()
        {
            /*
             Only form data values whose name doesn’t begin with an underscore are displayed. I explain why in the “Using the Anti-forgery
             Feature” section, later in this chapter.
             */
            foreach (string key in Request.Form.Keys.Where(k => !k.StartsWith("_")))
            {
                TempData[key] = string.Join(", ", Request.Form[key]);
            }
            return RedirectToAction(nameof(Results));
        }

        public IActionResult Results()
        {
            return View(TempData);
        }
    }
}
