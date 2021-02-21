using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolElectronicJournal.Interface;
using SchoolElectronicJournal.Models;
using SchoolElectronicJournal.Models.ViewModels;

namespace SchoolElectronicJournal.Controllers
{
    public class GradeController : Controller
    {
        IContain contain;
        public GradeController(IContain contain)
        {
            this.contain = contain;
        }
        public IActionResult List(int? stid, int? lesid, string userid)
        {
            if (stid != null && lesid != null)
            {
                var lessSt = contain.LessonStudents.Single(p => p.LessonId == lesid && p.StudentId == stid);
                var grade = contain.Grades.Where(i => i.LessonStudentId == lessSt.Id);
                ViewBag.LesStu = lessSt.Id;
                ViewBag.LesId = lesid;
                ViewBag.UserId = userid;
                

                return View(grade);
            }
            return NotFound();
        }
        public IActionResult Index(int? stid, int? lesid, string userid, int classid)
        {
            if (stid != null && lesid != null)
            {
                
                var lessSt = contain.LessonStudents.Single(p => p.LessonId == lesid && p.StudentId == stid);
                var grade = contain.Grades.Where(i => i.LessonStudentId == lessSt.Id);
                ViewBag.StId = stid;
                ViewBag.LesStu = lessSt.Id;
                ViewBag.LesId = lesid;
                ViewBag.UserId = userid;
                ViewBag.ClassId = classid;
                return View(grade);
            }
            
            return NotFound();
        }
       
        [HttpPost]
        public async Task<IActionResult> AddGrade(int id, Grade  grade, string userid, int classid)
        {
            var lesstu = contain.LessonStudents.Single(i => i.Id == id);
            lesstu.Grades = new List<Grade>();
                lesstu.Grades.Add(grade);
                await contain.AddGrade(grade);
            

            return RedirectToAction("Index", "Grade", new {stid = lesstu.StudentId, lesid = lesstu.LessonId, userid=userid, classid=classid });
        }
        public async Task<IActionResult> RemoveGrade(int id, string userid, int classid)
        {
            var grade = contain.Grades.Single(i => i.GradeId == id);
            await contain.RemoveGrade(grade);
            var less = contain.LessonStudents.Single(i => i.Id == grade.LessonStudentId);

            return RedirectToAction("Index", "Grade", new { stid = less.StudentId, lesid = less.LessonId, userid =userid, classid= classid });
        }
        
    }
}
