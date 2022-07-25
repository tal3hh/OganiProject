using System;
using System.Linq;
using OganiProject.Context;
using Microsoft.AspNetCore.Mvc;

namespace OganiProject.ViewComponents
{
    public class CategoryList : ViewComponent
    {
        readonly AppDbContext _context;

        public CategoryList(AppDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var list = _context.Categories.Where(x => x.Status != Entities.DataStatus.Deleted).ToList();

            return View(list);
        }
    }
}
