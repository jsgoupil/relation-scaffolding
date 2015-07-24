using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RelationScaffolding
{
    public class RelationAttribute : UIHintAttribute
    {
        public RelationAttribute(string uiHint = "Relation")
            : base(uiHint)
        {
        }
    }
}
