using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolElectronicJournal.Models;

namespace SchoolElectronicJournal.Interface
{
    public interface IContain
    {
       IQueryable<Student> Students { get; }
        IQueryable<Lesson> Lessons { get; }
        IQueryable<LessonStudent> LessonStudents { get; }
        IQueryable<Class> Classes { get;}
        IQueryable<Grade> Grades { get; }
        IQueryable<User> Users { get; }
      
        Task AddStudent(Student student);
        Task RemoveStudent(Student student);

        Task AddLesson(Lesson lesson);
        Task RemoveLesson(Lesson lesson);

        Task AddGrade(Grade grade);
        Task RemoveGrade(Grade grade);

        Task AddLesonStudent(LessonStudent lessonStudent);
        Task RemoveLessonStudent(LessonStudent lessonStudent);

        Task AddClass(Class @class);
        Task RemoveClass(Class @class);

        
        
        


    }
}
