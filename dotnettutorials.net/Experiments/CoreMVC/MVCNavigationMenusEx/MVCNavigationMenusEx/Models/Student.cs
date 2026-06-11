using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNavigationMenusEx.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Branch Branch { get; set; }
        public Gender Gender { get; set; }
        public string Address { get; set; }
        public IEnumerable<Gender> AllGenders { set; get; }
    }
}
