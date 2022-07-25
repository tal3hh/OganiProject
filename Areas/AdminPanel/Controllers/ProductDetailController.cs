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
    public class ProductDetailController : Controller
    {
        readonly AppDbContext _context;
        public ProductDetailController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ProductDetailList()
        {
            var list = await _context.ProductDetails.Where(x => x.Status != DataStatus.Deleted).Include(x => x.Product).ToListAsync();

            return View(list);
        }



        public async Task<IActionResult> Create()
        {
            var model = await _context.Products.Where(x => x.Status != DataStatus.Deleted).OrderByDescending(x => x.Id).ToListAsync();

            var details = new ProductDetail();

            return View((details, model));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] ProductDetail details)
        {
            if (!ModelState.IsValid)
            {
                return View(details);
            }
            await _context.ProductDetails.AddAsync(details);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProductDetailList", "ProductDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {
            var details = await _context.ProductDetails.FindAsync(id);
            var model = await _context.Products.Where(x => x.Status != DataStatus.Deleted).OrderByDescending(x => x.Id).ToListAsync();

            return View((details, model));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] ProductDetail details)
        {
            if (!ModelState.IsValid)
            {
                return View(details);
            }
            details.Status = DataStatus.Updated;
            details.ModifatedDate = DateTime.Now;

            _context.ProductDetails.Update(details);
            await _context.SaveChangesAsync();

            return RedirectToAction("ProductDetailList", "ProductDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var details = _context.ProductDetails.Find(id);

            _context.ProductDetails.Remove(details);

            await _context.SaveChangesAsync();

            return RedirectToAction("ProductDetailList", "ProductDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = await _context.ProductDetails.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
            return View(details);
        }
    }
}
