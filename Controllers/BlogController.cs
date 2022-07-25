using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OganiProject.Context;
using OganiProject.Entities;
using OganiProject.UniteOfWork;
using OganiProject.Utilities.Paginations;
using OganiProject.ViewModels.Admin.BlogViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OganiProject.Controllers
{
    public class BlogController : Controller
    {
        readonly IUow _uow;
        readonly AppDbContext _context;
        public BlogController(IUow uow, AppDbContext context = null)
        {
            _uow = uow;
            _context = context;
        }



        #region BlogPage
        public async Task<IActionResult> BlogPage(int page = 1, int take = 4)
        {

            var list = await _context.Blogs.Where(x => x.Status != DataStatus.Deleted).Include(x => x.BlogDetails)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();

            int count = await GetPageCount(take);
            var result = new Paginate<Blog>(list, page, count);

            return View(result);
        }
        private async Task<int> GetPageCount(int take)
        {
            var count = await _context.Blogs.CountAsync();

            return (int)Math.Ceiling((decimal)count / take);
        }
        #endregion



        #region BlogDetails
        public async Task<IActionResult> BlogDetailsPage(int id)
        {
            var blog = await _context.Blogs.Where(x => x.Status != DataStatus.Deleted).Include(x => x.Owner).Include(x => x.BlogDetails).FirstOrDefaultAsync(x => x.Id == id);

            return View(blog);
        }
        #endregion



        #region Blog Search 
        public async Task<IActionResult> BlogSearch(string search, int page = 1, int take = 4)
        {
            var blogs = from m in _context.Blogs.Where(x => x.Status != DataStatus.Deleted).Include(x=> x.BlogDetails) select m;

            if (!String.IsNullOrEmpty(search))
            {
                blogs = blogs.Where(x => x.Title.Contains(search));
            }

            var list = await blogs.Skip((page - 1) * take).Take(take).ToListAsync();

            int count = await GetPageCount(take);
            var result = new Paginate<Blog>(list, page, count);


            return View("BlogPage",result);
        }
        #endregion


    }
}
