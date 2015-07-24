using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [RelationScaffolding.RelationDisplay]
        public string Name { get; set; }

        public virtual ICollection<ActivityTeachers> ActivityTeachers { get; set; }
    }
}