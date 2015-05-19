using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Teacher : Person
    {
        [RelationScaffolding.RelationMultiple]
        public virtual ICollection<Course> Courses { get; set; }
    }
}