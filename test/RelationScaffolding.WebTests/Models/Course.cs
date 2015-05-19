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
        [RelationScaffolding.RelationEdit]
        public string Title { get; set; }
        public int TeacherId { get; set; }

        [RelationScaffolding.RelationSingle(Empty = "Select a teacher.")]
        public virtual Teacher Teacher { get; set; }
    }
}