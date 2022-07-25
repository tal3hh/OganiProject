using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OganiProject.Context;
using OganiProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OganiProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class BlogDetailController : Controller
    {
        readonly AppDbContext _context;
        public BlogDetailController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> BlogDetailList()
        {
            var list = await _context.BlogDetails.Where(x => x.Status != DataStatus.Deleted!).Include(x => x.Blog).ToListAsync();

            return View(list);
        }


        public async Task<IActionResult> Create()
        {
            var model = await _context.Blogs.Where(x => x.Status != DataStatus.Deleted!).ToListAsync();

            var details = new BlogDetail();

            return View((details, model));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] BlogDetail details)
        {
            if (!ModelState.IsValid)
            {
                return View(details);
            }
            await _context.BlogDetails.AddAsync(details);
            await _context.SaveChangesAsync();

            return RedirectToAction("BlogDetailList", "BlogDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {
            var details = await _context.BlogDetails.FindAsync(id);
            var model = await _context.Blogs.Where(x => x.Status != DataStatus.Deleted!).ToListAsync();

            return View((details, model));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] BlogDetail details)
        {
            if (!ModelState.IsValid)
            {
                return View(details);
            }
            details.Status = DataStatus.Updated;
            details.ModifatedDate = DateTime.Now;
            _context.BlogDetails.Update(details);

            await _context.SaveChangesAsync();

            return RedirectToAction("BlogDetailList", "BlogDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var details = _context.BlogDetails.Find(id);

            _context.BlogDetails.Remove(details);

            await _context.SaveChangesAsync();

            return RedirectToAction("BlogDetailList", "BlogDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = await _context.BlogDetails.Include(x => x.Blog).FirstOrDefaultAsync(x => x.Id == id);

            return View(details);
        }
    }
}
