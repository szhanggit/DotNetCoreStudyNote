using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WebApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class FormController : Controller
    {
        private DataContext context;

        public FormController(DataContext dbContext)
        {
            context = dbContext;
        }

        /*
         http://localhost:5000/controllers/Form/Index/5
         http://localhost:5000/controllers/form/header
         */
        public async Task<IActionResult> Index([FromQuery] long? id)                
        {
            ViewBag.Categories = new SelectList(context.Categories, "CategoryId", "Name");
            //return View("Form", await context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstAsync(p => p.ProductId == id));
            return View("Form", await context.Products.Include(p => p.Category).Include(p => p.Supplier).FirstOrDefaultAsync(p => id == null || p.ProductId == id));
        }

        //[HttpPost]
        //public IActionResult SubmitForm()
        //{
        //    /*
        //     Only form data values whose name doesn’t begin with an underscore are displayed. I explain why in the “Using the Anti-forgery
        //     Feature” section, later in this chapter.
        //     */
        //    foreach (string key in Request.Form.Keys.Where(k => !k.StartsWith("_")))
        //    {
        //        TempData[key] = string.Join(", ", Request.Form[key]);
        //    }
        //    return RedirectToAction(nameof(Results));
        //}

        /*
         http://localhost:5000/controllers/Form
         */
        //[HttpPost]
        //public IActionResult SubmitForm([Bind(Prefix = "Category")] Category category)
        //{
        //    TempData["category"] = System.Text.Json.JsonSerializer.Serialize(category);
        //    return RedirectToAction(nameof(Results));
        //}

        /*
         This example tells the model binding feature to look for values for the Name and Category properties, which excludes any other property from the process.
         */
        [HttpPost]
        public IActionResult SubmitForm(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                ModelState.AddModelError(nameof(Product.Name), "Enter a name");
            }
            /*
             The GetValidationState method is used to see whether there have been any errors recorded for a
             model property, either from the model binding process or because the AddModelError method has been called during explicit
             validation in the action method.
             Make sure  that model binder cannot convert into a decimal value.
             */
            if (ModelState.GetValidationState(nameof(Product.Price)) == ModelValidationState.Valid && product.Price < 1)
            {
                ModelState.AddModelError(nameof(Product.Price), "Enter a positive price");
            }
            if (!context.Categories.Any(c => c.CategoryId == product.CategoryId))
            {
                ModelState.AddModelError(nameof(Product.CategoryId), "Enter an existing category ID");
            }
            if (!context.Suppliers.Any(s => s.SupplierId == product.SupplierId))
            {
                ModelState.AddModelError(nameof(Product.SupplierId), "Enter an existing supplier ID");
            }
            if (ModelState.IsValid)
            {
                TempData["name"] = product.Name;
                TempData["price"] = product.Price.ToString();
                TempData["categoryId"] = product.CategoryId.ToString();
                TempData["supplierId"] = product.SupplierId.ToString();
                return RedirectToAction(nameof(Results));
            }
            else
            {
                return View("Form");
            }
        }

        public IActionResult Results()
        {
            return View(TempData);
        }

        //public string Header([FromHeader] string accept)
        //{
        //    return $"Header: {accept}";
        //}

        public string Header([FromHeader(Name = "Accept-Language")] string accept)
        {
            return $"Header: {accept}";
        }

        /*
         http://localhost:5000/controllers/form/body
         {"Name":"Soccer Boots", "Price":89.99}
         */
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public Product Body([FromBody] Product model)
        {
            return model;
        }
    }
}
