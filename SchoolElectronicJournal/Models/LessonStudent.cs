using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models
{

    
    public class LessonStudent
    {
        public int Id { get; set; }
        public int? LessonId { get; set; }
        public int? StudentId { get; set; }
       
        public virtual ICollection<Grade> Grades { get; set; } = new List<Grade>();

        public Student Student { get; set; }
        public Lesson Lesson { get; set; }

       
        
    }
}
