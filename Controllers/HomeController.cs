using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OganiProject.Models;
using OganiProject.UniteOfWork;
using OganiProject.Context;
using OganiProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace OganiProject.Controllers
{
    public class HomeController : Controller
    {
        readonly IUow _uow;
        readonly AppDbContext _context;
        public HomeController(IUow uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }



        public async Task<IActionResult> HomePage()
        {
            var categories = await _uow.GetRepository<Category>().GetAllOrderByAsync(x => x.Id, false);
            var products = await _context.Products.Where(x => x.Status != DataStatus.Deleted).Include(x => x.Category).ToListAsync();
            var blogs = await _context.Blogs.Where(x => x.Status != DataStatus.Deleted).Include(x => x.BlogDetails).OrderByDescending(x => x.Id).ToListAsync();
            var advert = await _uow.GetRepository<Advert>().GetAllOrderByAsync(x => x.Id, false);

            return View((categories, products, blogs, advert));
        }

        

    }

}
