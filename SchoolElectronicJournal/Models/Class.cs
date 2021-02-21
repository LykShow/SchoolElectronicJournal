using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolElectronicJournal.Models
{
    public class Class
    {
        public int ClassId { get; set; }
        public string Name { get; set; }
       // public int UserId { get; set; }
        public User User { get; set; }
        public virtual ICollection<Student> Students { get; set; } = new List<Student>();
        
    }
}
