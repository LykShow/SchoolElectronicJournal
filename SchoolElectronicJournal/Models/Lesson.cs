using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models
{
    public class Lesson
    {
        public int LessonId {get;set;}
        public string Name { get; set; }
        //public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<LessonStudent> LessonStudents { get; set; } = new List<LessonStudent>();

    }
}
