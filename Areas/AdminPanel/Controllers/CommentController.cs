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
    [Authorize(Roles ="Admin")]
    public class CommentController : Controller
    {
        readonly AppDbContext _context;
        public CommentController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CommentList()
        {
            var list = await _context.Comments.Where(x => x.Status != DataStatus.Deleted!).Include(x => x.Product).ToListAsync();

            return View(list);
        }



        public async Task<IActionResult> Update(int id)
        {
            var details = await _context.Comments.FindAsync(id);
            var saleoff = await _context.SaleOffs.Where(x => x.Status != DataStatus.Deleted!).ToListAsync();

            return View((details, saleoff));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] Comment details)
        {
            if (!ModelState.IsValid)
            {
                return View(details);
            }
            details.Status = DataStatus.Updated;
            details.ModifatedDate = DateTime.Now;
            _context.Comments.Update(details);
            await _context.SaveChangesAsync();

            return RedirectToAction("CommentList", "Comment", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var details = await _context.Comments.FindAsync(id);

            //_context.ProductDetails.Remove(details);

            details.Status = DataStatus.Deleted;
            details.ModifatedDate = DateTime.Now;
            _context.Comments.Update(details);
            await _context.SaveChangesAsync();

            return RedirectToAction("CommentList", "Comment", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = await _context.Comments.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
            return View(details);
        }
    }
}
