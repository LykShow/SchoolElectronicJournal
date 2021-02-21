using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SchoolElectronicJournal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace SchoolElectronicJournal.Interface
{
    public class ApplicationContext: IdentityDbContext<User>
    {
      
        public DbSet<Student> Students { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonStudent> LessonStudent { get; set; }
        public DbSet<Grade> Grade { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<User> User { get; set; }
       
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options)
        {
           // Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
           
           
            base.OnModelCreating(builder);
        }
    }
}
