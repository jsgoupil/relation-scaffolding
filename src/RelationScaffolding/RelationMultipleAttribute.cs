using System;
using System.ComponentModel.DataAnnotations;

namespace RelationScaffolding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RelationMultipleAttribute : RelationAttribute 
    {
        public RelationMultipleAttribute()
            : base("RelationMultiple")
        {
        }

        public bool CanAdd
        {
            get;
            set;
        }
    }
}
