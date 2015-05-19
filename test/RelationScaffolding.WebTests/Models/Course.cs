using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Course
    {
        public int Id { get; set; }

        [RelationScaffolding.RelationDisplay]
        public string Title { get; set; }
        public int TeacherId { get; set; }

        [RelationScaffolding.RelationSingle]
        public virtual Teacher Teacher { get; set; }
    }
}