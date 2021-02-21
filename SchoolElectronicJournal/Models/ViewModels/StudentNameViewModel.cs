using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models.ViewModels
{
    public class StudentNameViewModel
    {
        public IEnumerable<Student> Students { get; set; }
        public int Names { get; set; }
    }
}
