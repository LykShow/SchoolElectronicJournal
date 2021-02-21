using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolElectronicJournal.Interface;
using SchoolElectronicJournal.Models;
using SchoolElectronicJournal.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SchoolElectronicJournal.Controllers
{
    public class StudentController : Controller
    {
        private readonly IContain contain;

        public StudentController(IContain contain)
        {
            this.contain = contain;
        }
        
        public IActionResult Index(int id, string userid, string searchString)
        {
            var use = contain.Users.Single(x => x.Id == userid);
            var students  = contain.Students.Where(i=>i.ClassId==id&&i.User==use);
            if (!String.IsNullOrEmpty(searchString))
            {
                students =  contain.Students.Where(p => (p.Name.Contains(searchString) || p.Soname.Contains(searchString))&&p.ClassId==id);
            }
            ViewBag.Class = id;
            ViewBag.UserId = userid;
            return View("List",students);
        }

        
        public IActionResult Details(int id)
        {
            return View();
        }

       
        public IActionResult Create(int id, string userid)
        {
            ViewBag.Class = id;
            ViewBag.UserId = userid;
            return View("Index");
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, string userid, Student student)
        {
            var use = contain.Users.Single(x => x.Id == userid);
            var lesson = contain.Classes.Single(c => c.ClassId == id&&c.User==use);
            student.User = use;
            lesson.Students.Add(student);
            
            await contain.AddStudent(student);
            return RedirectToAction("Index", "Student", new { userid=userid,id=id, searchString=""});
           
        }

        
        public IActionResult Edit(int id)
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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

                           
       
        public async Task<IActionResult> Delete(int id, string userid)
        {
            try
            {
                var use = contain.Users.Single(x => x.Id == userid);
                var student = contain.Students.Where(p => p.StudentID == id&&p.User==use).Single();
                await contain.RemoveStudent(student);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Select(int id, string userid)
        {
            var lessonstudent = contain.LessonStudents.Where(x => x.StudentId == id);
            List<Lesson> lesson = new List<Lesson>();
            var use = contain.Users.Single(x => x.Id == userid);
            foreach (var s in lessonstudent)
            {
                
                lesson.Add(contain.Lessons.Where(x => x.LessonId == s.LessonId&&x.User==use).Single());
            }
            ViewBag.StId = id;
            ViewBag.UserId = userid;
            return View(lesson);
        }
       
    }
}
