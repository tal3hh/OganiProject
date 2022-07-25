using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OganiProject.Context;
using OganiProject.Entities;
using OganiProject.UniteOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OganiProject.Utilities.Paginations;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OganiProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        readonly IUow _uow;
        readonly IWebHostEnvironment _env;
        readonly AppDbContext _context;
        public ProductController(IUow uow, IWebHostEnvironment env, AppDbContext context)
        {
            _uow = uow;
            _env = env;
            _context = context;
        }

        public async Task<IActionResult> ProductList(string sortOrder, int page = 1, int take = 8)
        {

            ViewBag.Price = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";

            ViewBag.Count = sortOrder=="count_asc" ? "count_desc" : "count_asc";

            


            var Products = from b in _context.Products.Where(x => x.Status != DataStatus.Deleted).Include(x => x.Category)
                          select b;

            switch (sortOrder)
            {
                //case "name_desc":
                //    Products = Products.OrderByDescending(x => x.Name);
                //    break;
                //case "name_asc":
                //    Products = Products.OrderBy(x => x.Name);
                //    break;
                case "count_desc":
                    Products = Products.OrderByDescending(x => x.Count);
                    break;
                case "count_asc":
                    Products = Products.OrderBy(x => x.Count);
                    break;
                case "price_desc":
                    Products = Products.OrderByDescending(x => x.Price);
                    break;
                default:
                    Products = Products.OrderBy(x => x.Price);
                    break;
            }


            int count = await GetPageCount(take);

            var ProductList = await Products.Skip((page - 1) * take).Take(take).ToListAsync();

            Paginate<Product> result = new Paginate<Product>(ProductList, page, count);

            return View(result);
        }

        private async Task<int> GetPageCount(int take)
        {
            var count = await _context.Products.CountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }


        #region Create 
        public async Task<IActionResult> Create()
        {
            var list = await _uow.GetRepository<Category>().GetAllOrderByAsync(x => x.Id, false);

            return View((new Product(), list));
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind(Prefix = "Item1")] Product model)
        {
            if (!ModelState.IsValid)
                return View();
            if (!model.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("", "Sekil formatinda bir fayl secin.");
                return View();
            }


            string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", fileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
            }
            model.Image = fileName;

            await _uow.GetRepository<Product>().CreateAsync(model);
            await _uow.SaveChangeAsync();

            return RedirectToAction("ProductList", "Product", new { area = "AdminPanel" });
        }

        #endregion


        #region Update
        public async Task<IActionResult> Update(int id)
        {
            var category = await _uow.GetRepository<Product>().FindAsync(id);
            var list = await _uow.GetRepository<Category>().GetAllOrderByAsync(x => x.Id, false);
            return View((category, list));
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, [Bind(Prefix = "Item1")] Product model)
        {
            var Dbentity = await _uow.GetRepository<Product>().FindAsync(id);

            if (!ModelState.IsValid) return View(model);

            if (!model.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("", "Sekil formatinda bir fayl secin.");
                return View(model);
            }

            string oldPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", Dbentity.Image);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
            string newPath = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", fileName);
            using (FileStream stream = new FileStream(newPath, FileMode.Create))
            {
                await model.Photo.CopyToAsync(stream);
            }

            Dbentity.Image = fileName;
            Dbentity.Name = model.Name;
            Dbentity.Status = DataStatus.Updated;
            Dbentity.ModifatedDate = DateTime.Now;
            Dbentity.CategoryId = model.CategoryId;

            await _uow.SaveChangeAsync();
            return RedirectToAction("ProductList", "Product", new { area = "AdminPanel" });
        }
        #endregion


        #region Delete 
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _uow.GetRepository<Product>().FindAsync(id);
            if (entity == null) return NotFound();
            string path = Path.Combine(_env.WebRootPath, "AdminPanel/img/product", entity.Image);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            //_uow.GetRepository<Category>().Delete(entity);

            entity.Image = string.Empty;
            entity.Status = DataStatus.Deleted;
            entity.ModifatedDate = DateTime.Now;

            await _uow.SaveChangeAsync();

            return RedirectToAction("ProductList", "Product", new { area = "AdminPanel" });
        }
        #endregion


        #region Details
        public async Task<IActionResult> Details(int id)
        {
            var entity = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            return View(entity);
        }
        #endregion

    }
}
