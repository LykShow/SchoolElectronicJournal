using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models.ViewModels
{
    public class LessonStGradeViewModel
    {
        public Lesson CurrentLes { get; set; }
        public IEnumerable<Student> Students {get;set;}
        public SelectList Class { get; set; }
    }
}
