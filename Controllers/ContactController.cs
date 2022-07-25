using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OganiProject.Entities;
using OganiProject.UniteOfWork;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OganiProject.Controllers
{
    public class ContactController : Controller
    {
        readonly IUow _uow;

        public ContactController(IUow uow)
        {
            _uow = uow;

        }

        public async Task<IActionResult> ContactPage()
        {
            var contact = await _uow.GetRepository<Contact>().GetAllOrderByAsync(x => x.Id, false);

            return View(contact);
        }
    }

}
