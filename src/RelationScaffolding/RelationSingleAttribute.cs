using System;
using System.ComponentModel.DataAnnotations;

namespace RelationScaffolding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RelationSingleAttribute : RelationAttribute
    {
        public RelationSingleAttribute()
            : base("RelationSingle")
        {
        }

        public string Empty
        {
            get;
            set;
        }
    }
}
