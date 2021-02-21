using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models
{
    public class User: IdentityUser
    {
        
       
        public string Name { get; set; }
        
        public string SoName { get; set; }
       
        
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
       
    }
}
