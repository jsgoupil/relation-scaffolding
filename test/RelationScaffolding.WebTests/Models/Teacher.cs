using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Teacher : Person
    {
        [RelationScaffolding.RelationMultiple(CanAdd = true)] // Will add to the course under the RelationEdit
        public virtual ICollection<Course> Courses { get; set; }

        [RelationScaffolding.RelationMultiple(Many2Many = true)]
        public virtual ICollection<ActivityTeachers> ActivityTeachers { get; set; }
    }
}