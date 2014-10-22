using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace RelationScaffolding
{
    class RelationModelBinder : IModelBinder
    {
        public virtual object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            ValueProviderResult valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            object model = CreateModel(controllerContext, bindingContext, bindingContext.ModelType);
            var relationMemberLookup = new RelationMemberLookup(model);
            ((System.Reflection.PropertyInfo)relationMemberLookup.KeyMemberInfo).SetValue(model, valueProviderResult.AttemptedValue);
            return model;
        }

        protected virtual object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            // fallback to the type's default constructor
            Type typeToCreate = modelType;

            try
            {
                return Activator.CreateInstance(typeToCreate);
            }
            catch (MissingMethodException)
            {
                throw;
            }
        }
    }
}
