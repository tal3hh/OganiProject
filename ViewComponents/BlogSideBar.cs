using System;
using System.Linq;
using OganiProject.Context;
using Microsoft.AspNetCore.Mvc;

namespace OganiProject.ViewComponents
{
    public class BlogSideBar : ViewComponent
    {
        readonly AppDbContext _context;
        public BlogSideBar(AppDbContext context)
        {
            _context = context;
        }


        public IViewComponentResult Invoke()
        {
            var categories = _context.Categories.Where(x => x.Status != Entities.DataStatus.Deleted).OrderByDescending(x => x.Id).ToList();
            var blogs = _context.Blogs.Where(x => x.Status != Entities.DataStatus.Deleted).OrderByDescending(x => x.Id).ToList();
            var products = _context.Products.Where(x => x.Status != Entities.DataStatus.Deleted).OrderByDescending(x => x.Id).ToList();

            return View((categories, blogs, products));
        }
    }
}
