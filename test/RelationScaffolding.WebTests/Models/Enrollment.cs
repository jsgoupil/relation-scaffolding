using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Enrollment
    {
        public int Identification { get; set; }

        public int CourseId { get; set; }
        public int StudentId { get; set; }

        [RelationScaffolding.RelationDisplay]
        public virtual Course Course { get; set; }
        public virtual Student Student { get; set; }
    }
}