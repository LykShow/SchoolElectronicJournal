using Microsoft.EntityFrameworkCore;
using SchoolElectronicJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Interface
{
    public class EFRepository : IContain
    {
        public IQueryable<Student> Students => context.Students;

        public IQueryable<Lesson> Lessons => context.Lessons;

        public IQueryable<LessonStudent> LessonStudents => context.LessonStudent;

        public IQueryable<Grade> Grades => context.Grade;

        public IQueryable<Class> Classes => context.Class;

        public IQueryable<User> Users => context.Users;

        private ApplicationContext context;
        public EFRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Task AddStudent(Student student)
        {
            context.Add(student);
            return context.SaveChangesAsync();
        }

        public Task RemoveStudent(Student student)
        {
            context.Remove(student);
            return context.SaveChangesAsync();
        }

        public Task AddLesson(Lesson lesson)
        {
            context.Add(lesson);
            return context.SaveChangesAsync();
        }

        public Task RemoveLesson(Lesson lesson)
        {
            context.Remove(lesson);
            return context.SaveChangesAsync();
        }

        public Task AddGrade(Grade grade)
        {
            context.Add(grade);
            return context.SaveChangesAsync();
        }

        public Task RemoveGrade(Grade grade)
        {
            context.Remove(grade);
            return context.SaveChangesAsync();
        }

        public Task AddLesonStudent(LessonStudent lessonStudent)
        {
            context.Add(lessonStudent);
            return context.SaveChangesAsync();
        }

        public Task AddClass(Class @class)
        {
            context.Add(@class);
            return context.SaveChangesAsync();
        }

        public Task RemoveClass(Class @class)
        {
            context.Remove(@class);
            return context.SaveChangesAsync();
        }

        public Task RemoveLessonStudent(LessonStudent lessonStudent)
        {
            context.Remove(lessonStudent);
            return context.SaveChangesAsync();
        }

        
    }
}
