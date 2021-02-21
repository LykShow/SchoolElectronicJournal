using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolElectronicJournal.Models;
using SchoolElectronicJournal.Interface;
using Microsoft.EntityFrameworkCore;
using SchoolElectronicJournal.Filtres;

namespace SchoolElectronicJournal.Controllers
{
    [SimpleResourceFilter]
    public class ClassController : Controller
    {
        private readonly IContain contain;
        public ClassController(IContain contain)
        {
            this.contain = contain;
        }
        public async Task<IActionResult> Index(string id)
        {
           var use = contain.Users.Where(x => x.Id == id).Single();
            var cl = contain.Classes.Where(x=>x.User==use);
            
            ViewBag.UserId = id;
            return View(await cl.ToListAsync());
        }

        
        public ActionResult Details(int id)
        {
            return View();
        }

        
        public IActionResult Create(string id)
        {
            ViewBag.UserId = id;
            return View();
        }

        // POST: ClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Class @class, string id)
        {
            if (ModelState.IsValid)
            {
                var use = contain.Users.Single(x => x.Id == id);
                @class.User = use;
                await contain.AddClass(@class);
                return RedirectToAction("Index", "Class", new { id = id });
            }
            ViewBag.UserId = id;
            return View(@class);
        }

        // GET: ClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClassController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

                    
       
        
        public async Task<IActionResult> Delete(int id)
        {
            var clas = contain.Classes.Single(x => x.ClassId == id);
           
                await contain.RemoveClass(clas);
                return RedirectToAction(nameof(Index));
           
        }
    }
}
