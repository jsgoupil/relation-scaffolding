using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class ActivityTeachers
    {
        public int Id { get; set; }

        [RelationScaffolding.RelationDisplay]
        public string RegistrationCode { get; set; }

        public int TeacherId { get; set; }
        public int ActivityId { get; set; }

        [RelationScaffolding.RelationList(PropertyName = "TeacherId")]
        public virtual Teacher Teacher { get; set; }

        [RelationScaffolding.RelationList(PropertyName = "ActivityId")]
        public virtual Activity Activity { get; set; }
    }
}