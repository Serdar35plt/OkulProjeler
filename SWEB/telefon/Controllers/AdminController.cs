using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using telefon.Models;

namespace telefon.Controllers
{
    //[Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly Gvt context;

        public AdminController(ILogger<AdminController> logger, Gvt context)
        {
            _logger = logger;
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {
            var messages = await context.contacts.OrderByDescending(c => c.Id).ToListAsync();
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var msg = await context.contacts.FindAsync(id);
            if (msg == null) return NotFound();

            msg.IsRead = true;
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}