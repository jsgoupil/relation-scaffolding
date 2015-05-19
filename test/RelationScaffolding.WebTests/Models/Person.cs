using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RelationScaffolding.WebTests.Models
{
    public class Person
    {
        public int Id { get; set; }

        [RelationScaffolding.RelationDisplay]
        public string Name { get; set; }
    }
}