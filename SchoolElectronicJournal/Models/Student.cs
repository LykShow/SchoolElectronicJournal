using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Soname { get; set; }
        public int Age { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
       // public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<LessonStudent> LessonStudents { get; set; } = new List<LessonStudent>();
       
      
        
         
       
    }
}
