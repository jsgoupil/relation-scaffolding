using System;
using System.ComponentModel.DataAnnotations;

namespace RelationScaffolding
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RelationOne2OneAttribute : UIHintAttribute
    {
        public RelationOne2OneAttribute()
            : base("RelationOne2One")
        {
        }

        public string Empty
        {
            get;
            set;
        }
    }
}
