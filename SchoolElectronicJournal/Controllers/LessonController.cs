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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolElectronicJournal.Controllers
{
    public class LessonController : Controller
    {
        IContain contain;
        public LessonController(IContain contain)
        {
            this.contain = contain;
        }
       
        public async Task<IActionResult> Index(string id, string searchString)
        {
          
            var use = contain.Users.Single(x => x.Id == id);
            var lesson = contain.Lessons.Where(l=>l.User==use);
            ViewBag.UserId = id;
            if (!String.IsNullOrEmpty(searchString))
            {
                //
                lesson= contain.Lessons.Where(p => p.Name.Contains(searchString)&&p.User==use);
                return View(await lesson.ToListAsync());
            }
            return View(await lesson.ToListAsync());
            
        }

        
        public ActionResult Details(int id)
        {
            return View();
        }

        
        public IActionResult Create(string id)
        {
            var lesson = new Lesson();
            lesson.LessonStudents = new List<LessonStudent>();
            PopulateAssignedCourseData(lesson, id);
            ViewBag.UserId = id;
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lesson lesson, int[] selectedCourses,string id)
        {
            ViewBag.UserId = id;
            if (ModelState.IsValid)
            {
                lesson.LessonStudents = new List<LessonStudent>();
                var use = contain.Users.Single(x => x.Id == id);
                lesson.User = use;

                foreach (var student in selectedCourses)
                    {
                        var lessonstudent = new LessonStudent { LessonId = lesson.LessonId, StudentId = student };
                        lesson.LessonStudents.Add(lessonstudent);
                    }

                
                await contain.AddLesson(lesson);
                PopulateAssignedCourseData(lesson, id);
                return RedirectToAction("Index", "Lesson", new {id=id });
            }
            return View(lesson);
            
        }
        private void PopulateAssignedCourseData(Lesson lesson, string id)
        {
            //
            var use = contain.Users.Single(x => x.Id == id);
            var lesst = contain.Students.Where(s=>s.User==use);
           
            var viewModel = new List<LessonStudentViewModel>();
            foreach (var course in lesst)
            {
                viewModel.Add(new LessonStudentViewModel
                {
                    StudentId = course.StudentID,
                    Name = course.Name,
                    SoName= course.Soname,
                   
                });
            }
            ViewData["Courses"] = viewModel;
        }


        public async Task<IActionResult> Edit(int? id, string userid)
        {
            if (id == null)
            {
                return NotFound();
            }
            var use = contain.Users.Single(x => x.Id == userid);
            
            var lesson = await contain.Lessons.FirstOrDefaultAsync(x => x.LessonId == id&&x.User==use);
            ViewBag.UserId = userid;
            UpdateLessonStudent(lesson, userid);

            return View(lesson);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string userid, int [] selectedStudent)
        {
            
            var use = contain.Users.Single(x => x.Id == userid);
            var lesson = contain.Lessons.Where(i => i.LessonId == id&&i.User==use).FirstOrDefault();
            
           
                foreach (var s in selectedStudent)
                {
                  await contain.AddLesonStudent(new LessonStudent { LessonId = lesson.LessonId, StudentId = s });

                }
               
                return RedirectToAction("Index", "Lesson", new { id = userid });
           
        }
        private void UpdateLessonStudent(Lesson lesson, string id)
        {
            
            var lessonstudent = contain.LessonStudents.Where(l => l.LessonId ==lesson.LessonId);
            List<Student> students = new List<Student>();
            //
            var use = contain.Users.Single(x => x.Id == id);
            students = contain.Students.Where(s=>s.User==use).ToList();
            foreach (var les in lessonstudent)
            {
                students.Remove(contain.Students.Where(x => x.StudentID == les.StudentId).Single());
            }
            var viewModel = new List<LessonStudentViewModel>();
            foreach (var st in students)
            {
                viewModel.Add(new LessonStudentViewModel
                {
                    StudentId = st.StudentID,
                    Name = st.Name,
                    SoName = st.Soname,
                   
                }) ;
            }
            ViewBag.CurrentStudent = viewModel;

        }

                     
        
       
        public async Task<IActionResult> Delete(int id, string userid)
        {
            var use = contain.Users.Single(x => x.Id == userid);
            var lesson = contain.Lessons.Where(p => p.LessonId == id&&p.User==use).Single();
                await contain.RemoveLesson(lesson);
                return RedirectToAction("Index", "Lesson", new { id = userid });
            
           
        }
        public IActionResult Select(string userid, int id, int? @class, string searchString)
        {
            var lessonst = contain.LessonStudents.Where(p => p.LessonId == id);
            var use = contain.Users.Single(x => x.Id == userid);
            List<Student> students = new List<Student>();
            foreach(var s in lessonst)
            {
                students.Add(contain.Students.Where(p => p.StudentID == s.StudentId).Single());
                
            }
            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(x => x.Name.Contains(searchString) || x.Soname.Contains(searchString)).ToList();
            }
            if (@class != null && @class != 0)
            {
                students = students.Where(x => x.ClassId == @class).ToList();
            }

            List<Class> classes = contain.Classes.Where(x=>x.User==use).ToList();
            
            classes.Insert(0, new Class { ClassId = 0, Name = "All" });
            ViewBag.UserId = userid;
            ViewBag.ClassId = @class;
            var lesson = new LessonStGradeViewModel() { CurrentLes = contain.Lessons.Where(p => p.LessonId == id&&p.User==use).Single(), Students = students, Class = new SelectList(classes, "ClassId", "Name") };
            

            return View(lesson);
        }


        public async Task<IActionResult> RemoveStudent(int lesid, int stid, string userid)
        {
            var lessst = contain.LessonStudents.Single(x => x.LessonId == lesid && x.StudentId == stid);
            await contain.RemoveLessonStudent(lessst);

            return RedirectToAction("Select", "Lesson", new { id= lesid, userid=userid, searchString ="", @class = 0});
        }
        
    }
}
