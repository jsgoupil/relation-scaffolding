using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Student : Person
    {
        [RelationScaffolding.RelationMultiple]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}