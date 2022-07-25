using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OganiProject.Entities;
using OganiProject.UniteOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OganiProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class ContactController : Controller
    {
        readonly IUow _uow;
        readonly IWebHostEnvironment _env;
        public ContactController(IUow uow, IWebHostEnvironment env)
        {
            _uow = uow;
            _env = env;
        }

        public async Task<IActionResult> ContactList()
        {
            var list = await _uow.GetRepository<Contact>().GetAllOrderByAsync(x => x.Id, false);

            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Contact model)
        {
            if (!ModelState.IsValid)
                return View();

            await _uow.GetRepository<Contact>().CreateAsync(model);
            await _uow.SaveChangeAsync();

            return RedirectToAction("ContactList", "Contact", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Update(int id)
        {
            var entity = await _uow.GetRepository<Contact>().FindAsync(id);
            return View(entity);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, Contact model)
        {
            var Dbentity = await _uow.GetRepository<Contact>().FindAsync(id);

            Dbentity.Email = model.Email;
            Dbentity.Phone = model.Phone;
            Dbentity.Adress = model.Adress;
            Dbentity.OpenTime = model.OpenTime;
            Dbentity.Status = DataStatus.Updated;
            Dbentity.ModifatedDate = DateTime.Now;

            await _uow.SaveChangeAsync();
            return RedirectToAction("ContactList", "Contact", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _uow.GetRepository<Contact>().FindAsync(id);
            if (entity == null) return NotFound();

            //_uow.GetRepository<Category>().Delete(entity);

            entity.Status = DataStatus.Deleted;
            entity.ModifatedDate = DateTime.Now;
            await _uow.SaveChangeAsync();

            return RedirectToAction("ContactList", "Contact", new { area = "AdminPanel" });
        }

        public async Task<IActionResult> Details(int id)
        {
            var entity = await _uow.GetRepository<Contact>().FindAsync(id);
            return View(entity);
        }
    }
}
