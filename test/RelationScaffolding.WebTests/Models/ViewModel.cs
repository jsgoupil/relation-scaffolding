using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class ViewModel
    {
        public Teacher Teacher { get; set; }
        public Course Course { get; set; }
        public ICollection<Course> AvailableCourses { get; set; }
        public ICollection<Teacher> AvailableTeachers { get; set; }
        public Student Student { get; set; }
    }
}