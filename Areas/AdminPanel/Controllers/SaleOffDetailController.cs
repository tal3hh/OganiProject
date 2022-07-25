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
    public class SaleOffDetailController : Controller
    {
        readonly AppDbContext _context;
        public SaleOffDetailController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> SaleOffDetailList()
        {
            var list = await _context.SaleOffDetails.Where(x => x.Status != DataStatus.Deleted).Include(x => x.SaleOff).ToListAsync();

            return View(list);
        }


        public async Task<IActionResult> Create()
        {
            var model = await _context.SaleOffs.Where(x => x.Status != DataStatus.Deleted).ToListAsync();

            var details = new SaleOffDetail();

            return View((details, model));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] SaleOffDetail details)
        {
            if (!ModelState.IsValid)
            {
                return View(details);
            }
            await _context.SaleOffDetails.AddAsync(details);
            await _context.SaveChangesAsync();

            return RedirectToAction("SaleOffDetailList", "SaleOffDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {
            var details = await _context.SaleOffDetails.FindAsync(id);
            var model = await _context.SaleOffs.Where(x => x.Status != DataStatus.Deleted).ToListAsync();

            return View((details, model));
        }
        [HttpPost]
        public async Task<IActionResult> Update([Bind(Prefix = "Item1")] SaleOffDetail details)
        {
            if (!ModelState.IsValid)
            {
                return View(details);
            }
            details.Status = DataStatus.Updated;
            details.ModifatedDate = DateTime.Now;
            _context.SaleOffDetails.Update(details);
            await _context.SaveChangesAsync();

            return RedirectToAction("SaleOffDetailList", "SaleOffDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var details = _context.SaleOffDetails.Find(id);

            _context.SaleOffDetails.Remove(details);
            await _context.SaveChangesAsync();

            return RedirectToAction("SaleOffDetailList", "SaleOffDetail", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var details = await _context.SaleOffDetails.Include(x => x.SaleOff).FirstOrDefaultAsync(x => x.Id == id);
            return View(details);
        }
    }
}
