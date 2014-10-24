using System;
using System.ComponentModel.DataAnnotations;

namespace RelationScaffolding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RelationOne2ManyAttribute : RelationAttribute 
    {
        public RelationOne2ManyAttribute()
            : base("RelationOne2Many")
        {
        }

        public bool CanAdd
        {
            get;
            set;
        }
    }
}
