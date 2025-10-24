using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using telefon.Models;

namespace telefon.Controllers
{
    //[Route("[controller]")]
    public class ContactsController : Controller
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly Gvt context;

        public ContactsController(ILogger<ContactsController> logger, Gvt _Context)
        {
            _logger = logger;
            context = _Context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Contacts model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) || string.IsNullOrWhiteSpace(model.Message))
        {
            TempData["Error"] = "Lütfen isim ve mesaj alanlarını doldurun.";
            return RedirectToAction("Index");
        }

        context.contacts.Add(model);
        await context.SaveChangesAsync();

        TempData["Success"] = "Mesajınız başarıyla gönderildi.";
        return RedirectToAction("Index"); // ✅ Redirect (PRG)
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}