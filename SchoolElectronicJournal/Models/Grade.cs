using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int Number { get; set; }
        public int LessonStudentId { get; set; }
        public LessonStudent LessonStudent { get; set; }
    }
}
