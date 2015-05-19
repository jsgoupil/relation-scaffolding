using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Enrollment
    {
        [Key]
        public int Identification { get; set; }

        public int CourseId { get; set; }
        public int StudentId { get; set; }

        [RelationScaffolding.RelationDisplay]
        [RelationScaffolding.Relation]
        public virtual Course Course { get; set; }

        [RelationScaffolding.Relation]
        public virtual Student Student { get; set; }
    }
}