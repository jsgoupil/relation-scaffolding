using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace RelationScaffolding
{
    class RelationModelMetadataProvider : CachedDataAnnotationsModelMetadataProvider
    {
        protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
        {
            var value = new RelationModelMetadata(prototype, modelAccessor);
            value.RequestValidationEnabled = false;
            return value;
        }
    }
}
