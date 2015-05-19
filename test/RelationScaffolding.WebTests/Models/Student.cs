using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Student : Person
    {
        [RelationScaffolding.Relation]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}