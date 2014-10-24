using System.Web.Mvc;

namespace RelationScaffolding
{
    public class RelationModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var relationMemberLookup = new RelationMemberLookup(null, bindingContext.ModelType);
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + relationMemberLookup.KeyMemberInfo.Name);

            if (valueProviderResult != null)
            {
                object finalObject = null;

                var cache = new RelationCache(controllerContext.HttpContext);

                // We don't try to find cache if the id is empty
                if (valueProviderResult.AttemptedValue != "")
                {
                    // Let's try to get the same object we have returned before.
                    finalObject = cache.Retrieve(bindingContext.ModelType, valueProviderResult.AttemptedValue);
                }

                // We couldn't find the object in the cache.
                if (finalObject == null)
                {
                    finalObject = base.BindModel(controllerContext, bindingContext);
                }

                if (valueProviderResult.AttemptedValue != "")
                {
                    cache.Save(bindingContext.ModelType, valueProviderResult.AttemptedValue, finalObject);
                }

                return finalObject;
            }

            return null;
        }
    }
}
