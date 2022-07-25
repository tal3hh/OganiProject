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
    public class ShopController : Controller
    {
        readonly IUow _uow;
        readonly AppDbContext _context;
        public ShopController(IUow uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
        }



        public async Task<IActionResult> ShopPage(string sortOrder,int page = 1, int take = 6)
        {
            ViewBag.Name = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.Price = (sortOrder == "price_desc") ? "price_asc" : "price_desc";
            ViewBag.AllProducts = _context.Products.Count();

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
            var saleoff = await _uow.GetRepository<SaleOff>().GetAllOrderByAsync(x => x.Id, false);


            return View((saleoff, result));
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




    }
}
