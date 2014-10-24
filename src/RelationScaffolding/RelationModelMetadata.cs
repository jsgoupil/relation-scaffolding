using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RelationScaffolding
{
    class RelationModelMetadata : CachedDataAnnotationsModelMetadata
    {
        public RelationModelMetadata(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
            : base(prototype, modelAccessor)
        {
        }

        public override IEnumerable<ModelValidator> GetValidators(ControllerContext context)
        {
            return Enumerable.Empty<ModelValidator>();
        }
    }
}
