using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Html;

namespace WebApp.Components
{
    public class CitySummary : ViewComponent
    {
        private CitiesData data;
        public CitySummary(CitiesData cdata)
        {
            data = cdata;
        }
        //public string Invoke()
        //{
        //    return $"{data.Cities.Count()} cities, " + $"{data.Cities.Sum(c => c.Population)} people";
        //}

        //public IViewComponentResult Invoke()
        //{
        //    return View(new CityViewModel
        //    {
        //        Cities = data.Cities.Count(),
        //        Population = data.Cities.Sum(c => c.Population)
        //    });
        //}
        //public IViewComponentResult Invoke()
        //{
        //    return new HtmlContentViewComponentResult(new HtmlString("This is a <h3><i>string</i></h3>"));
        //}
        //public string Invoke()
        //{
        //    if (RouteData.Values["controller"] != null)
        //    {
        //        return "Controller Request";
        //    }
        //    else
        //    {
        //        return "Razor Page Request";
        //    }
        //}

        public IViewComponentResult Invoke(string themeName)
        {
            ViewBag.Theme = themeName;
            return View(new CityViewModel
            {
                Cities = data.Cities.Count(),
                Population = data.Cities.Sum(c => c.Population)
            });
        }
    }
}


/*
 24-5

HttpContext This property returns an HttpContext object that describes the current request and the response that is being
prepared.
Request This property returns an HttpRequest object that describes the current HTTP request.
User This property returns an IPrincipal object that describes the current user, as described in Chapters 37 and 38.
RouteData This property returns a RouteData object that describes the routing data for the current request.
ViewBag This property returns the dynamic view bag object, which can be used to pass data between the view component
and the view, as described in Chapter 22.
ModelState This property returns a ModelStateDictionary, which provides details of the model binding process, as described
in Chapter 29.
ViewData This property returns a ViewDataDictionary, which provides access to the view data provided for the view
component.
 */