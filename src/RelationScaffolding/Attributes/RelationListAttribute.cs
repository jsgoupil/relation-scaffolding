using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationScaffolding
{
    public class RelationListAttribute : Attribute
    {
        public string PropertyName
        {
            get;
            set;
        }
    }
}
