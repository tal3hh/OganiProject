using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OganiProject.Context;
using OganiProject.Entities;
using OganiProject.UniteOfWork;
using OganiProject.Utilities.Paginations;
using OganiProject.ViewModels.Admin.ProductViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OganiProject.Controllers
{
    public class ProductController : Controller
    {
        readonly IUow _uow;
        readonly AppDbContext _context;
        public ProductController(IUow uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }


        #region ProductList
        public async Task<IActionResult> ProductPage(string sortOrder,int page = 1, int take = 8)
        {
            ViewBag.Name = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Price = (sortOrder=="price_desc") ? "price_asc" : "price_desc";

            var Products = from b in _context.Products.Where(x => x.Status != DataStatus.Deleted)
                           select b;

            switch (sortOrder)
            {
                case "price_desc":
                    Products = Products.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    Products = Products.OrderBy(x => x.Price);
                    break;
                case "name_desc":
                    Products = Products.OrderByDescending(x => x.Name);
                    break;
                default:
                    Products = Products.OrderBy(x => x.Name);
                    break;
            }

            var list = await Products.Skip((page - 1) * take).Take(take).ToListAsync();

            var productsVM = GetMapDatas(list);
            int count = await GetPageCount(take);

            var result = new Paginate<ProductVM>(productsVM, page, count);


            //All Products Count
            ViewBag.AllProducts = _context.Products.Count();

            return View(result);
        }

        private async Task<int> GetPageCount(int take)
        {
            var count = await _context.Products.CountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }
        private List<ProductVM> GetMapDatas(List<Product> products)
        {
            List<ProductVM> productList = new List<ProductVM>();

            foreach (var product in products)
            {
                ProductVM newProduct = new ProductVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Image = product.Image,
                    Price = product.Price,
                    Count = product.Count,
                    Category = product.Category,
                    CategoryId = product.CategoryId
                };

                productList.Add(newProduct);
            }

            return productList;
        }
        #endregion


        #region ProductCategory
        public async Task<IActionResult> ProductCategoryPage(string sortOrder,int id, int page = 1, int take = 9)
        {
            ViewBag.Name = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Price = (sortOrder == "price_desc") ? "price_asc" : "price_desc";

            var CategoryProducts = from b in _context.Products
                           .Where(x => x.Status != DataStatus.Deleted)
                           .Where(x => x.CategoryId == id)
                           .Include(x => x.Category)
                           select b;

            switch (sortOrder)
            {
                case "price_desc":
                    CategoryProducts = CategoryProducts.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    CategoryProducts = CategoryProducts.OrderBy(x => x.Price);
                    break;
                case "name_desc":
                    CategoryProducts = CategoryProducts.OrderByDescending(x => x.Name);
                    break;
                default:
                    CategoryProducts = CategoryProducts.OrderBy(x => x.Name);
                    break;
            }

            var list = await CategoryProducts.Skip((page - 1) * take).Take(take).ToListAsync();

            var productsVM = GetMapDatas(list);
            int count = await GetPageCount(take);

            var result = new Paginate<ProductVM>(productsVM, page, count);


            //All Products Count
            ViewBag.AllProducts = CategoryProducts.Count();
            
            return View(result);
        }
        #endregion


        #region ProductDetails
        public async Task<IActionResult> ProductDetailsPage(int id)
        {
            try
            {
                var productdetails = await _context.Products.Where(x => x.Status != DataStatus.Deleted).Include(x => x.ProductDetails).FirstOrDefaultAsync(x => x.Id == id);
                if(productdetails.ProductDetails==null) return View("ExcptError");

                var productlist = await _uow.GetRepository<Product>().GetAllOrderByAsync(x => x.Id, false);

                var commentlist = await _context.Comments.Where(x => x.Status != DataStatus.Deleted).Where(x => x.ProductId == id).OrderByDescending(x => x.CreatedDate).ToListAsync();

                var comment = await _context.Comments.FindAsync(id);

                TempData["ProId"] = id;

                return View((productdetails, productlist, commentlist, comment));
            }
            catch (Exception ex)
            {

                return View("ExcptError");
            }
        }
        #endregion


        #region Comment
        [HttpPost]
        public async Task<IActionResult> Comment([Bind(Prefix ="Item4")]Comment model)
        {
            var id = TempData["ProId"];

            model.ProductId = (int)id;


            await _context.Comments.AddAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("HomePage","Home");
        }
        #endregion


        #region Search
        public async Task<IActionResult> ProductSearch(string search, string sortOrder, int page = 1, int take = 8)
        {

            var Products = from m in _context.Products.Where(x=> x.Status != DataStatus.Deleted) select m;

            if (!String.IsNullOrEmpty(search))
            {
                Products = Products.Where(x => x.Name.Contains(search));
            }
            
            ViewBag.Name = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Price = (sortOrder == "price_desc") ? "price_asc" : "price_desc";
            ViewBag.AllProducts = Products.Count();


            switch (sortOrder)
            {
                case "price_desc":
                    Products = Products.OrderByDescending(x => x.Price);
                    break;
                case "price_asc":
                    Products = Products.OrderBy(x => x.Price);
                    break;
                case "name_desc":
                    Products = Products.OrderByDescending(x => x.Name);
                    break;
                default:
                    Products = Products.OrderBy(x => x.Name);
                    break;
            }

            var list = await Products.Skip((page - 1) * take).Take(take).ToListAsync();

            var productsVM = GetMapDatas(list);
            int count = await GetPageCount(take);

            var result = new Paginate<ProductVM>(productsVM, page, count);
            

            return View("ProductPage",result );
        }
        #endregion 


    }

}

