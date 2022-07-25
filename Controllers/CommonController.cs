using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OganiProject.Controllers
{
    public class CommonController : Controller
    {
        
        public IActionResult BasketPage()
        {
            return View();
        }

        public IActionResult FavoriPage()
        {
            return View();
        }

      
    }
}
