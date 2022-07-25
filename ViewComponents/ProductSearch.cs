using System;
using System.Linq;
using OganiProject.Context;
using OganiProject.Entities;
using Microsoft.AspNetCore.Mvc;

namespace OganiProject.ViewComponents
{
    public class ProductSearch : ViewComponent
    {
        
        public IViewComponentResult Invoke()
        {
            var product = new Product();

            return View(product);
        }
    }
}
