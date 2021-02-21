using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models.ViewModels
{
    public class LessonSelectViewModel
    {
        public Lesson Lesson { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}
