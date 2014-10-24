using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace RelationScaffolding
{
    public class RelationModelBinder : DefaultModelBinder
    {
        protected override void BindProperty(ControllerContext controllerContext, ModelBindingContext bindingContext, System.ComponentModel.PropertyDescriptor propertyDescriptor)
        {
            // need to skip properties that aren't part of the request, else we might hit a StackOverflowException
            string fullPropertyKey = CreateSubPropertyName(bindingContext.ModelName, propertyDescriptor.Name);
            if (!bindingContext.ValueProvider.ContainsPrefix(fullPropertyKey))
            {
                return;
            }

            IModelBinder propertyBinder = null;
            Type listElementType = null;
            if (propertyDescriptor.Attributes.OfType<RelationScaffolding.RelationAttribute>().Any())
            {
                Type enumerableType = TypeHelpers.ExtractGenericInterface(propertyDescriptor.PropertyType, typeof(IEnumerable<>));
                if (enumerableType != null)
                {
                    listElementType = enumerableType.GetGenericArguments()[0];
                    ModelBinders.Binders.Add(listElementType, new InternalRelationModelBinder());
                    propertyBinder = Binders.GetBinder(propertyDescriptor.PropertyType);
                }
                else
                {
                    propertyBinder = new RelationScaffolding.InternalRelationModelBinder();
                }
            }
            else
            {
                // call into the property's model binder
                propertyBinder = Binders.GetBinder(propertyDescriptor.PropertyType);
            }

            object originalPropertyValue = propertyDescriptor.GetValue(bindingContext.Model);
            ModelMetadata propertyMetadata = bindingContext.PropertyMetadata[propertyDescriptor.Name];
            propertyMetadata.Model = originalPropertyValue;
            ModelBindingContext innerBindingContext = new ModelBindingContext()
            {
                ModelMetadata = propertyMetadata,
                ModelName = fullPropertyKey,
                ModelState = bindingContext.ModelState,
                ValueProvider = bindingContext.ValueProvider
            };
            object newPropertyValue = GetPropertyValue(controllerContext, innerBindingContext, propertyDescriptor, propertyBinder);
            propertyMetadata.Model = newPropertyValue;

            // Remove the type we added to the model type
            if (listElementType != null)
            {
                ModelBinders.Binders.Remove(listElementType);
            }

            // validation
            ModelState modelState = bindingContext.ModelState[fullPropertyKey];
            if (modelState == null || modelState.Errors.Count == 0)
            {
                if (OnPropertyValidating(controllerContext, bindingContext, propertyDescriptor, newPropertyValue))
                {
                    SetProperty(controllerContext, bindingContext, propertyDescriptor, newPropertyValue);
                    OnPropertyValidated(controllerContext, bindingContext, propertyDescriptor, newPropertyValue);
                }
            }
            else
            {
                SetProperty(controllerContext, bindingContext, propertyDescriptor, newPropertyValue);

                // Convert FormatExceptions (type conversion failures) into InvalidValue messages
                foreach (ModelError error in modelState.Errors.Where(err => String.IsNullOrEmpty(err.ErrorMessage) && err.Exception != null).ToList())
                {
                    for (Exception exception = error.Exception; exception != null; exception = exception.InnerException)
                    {
                        // We only consider "known" type of exception and do not make too aggressive changes here
                        if (exception is FormatException || exception is OverflowException)
                        {
                            string displayName = propertyMetadata.GetDisplayName();
                            string errorMessageTemplate = GetValueInvalidResource(controllerContext);
                            string errorMessage = String.Format(CultureInfo.CurrentCulture, errorMessageTemplate, modelState.Value.AttemptedValue, displayName);
                            modelState.Errors.Remove(error);
                            modelState.Errors.Add(errorMessage);
                            break;
                        }
                    }
                }
            }
        }

        private static string GetValueInvalidResource(ControllerContext controllerContext)
        {
            return GetUserResourceString(controllerContext, "PropertyValueInvalid") ?? "The value '{0}' is not valid for {1}."; // TODO?
        }

        // If the user specified a ResourceClassKey try to load the resource they specified.
        // If the class key is invalid, an exception will be thrown.
        // If the class key is valid but the resource is not found, it returns null, in which
        // case it will fall back to the MVC default error message.
        private static string GetUserResourceString(ControllerContext controllerContext, string resourceName)
        {
            string result = null;

            if (!String.IsNullOrEmpty(ResourceClassKey) && (controllerContext != null) && (controllerContext.HttpContext != null))
            {
                result = controllerContext.HttpContext.GetGlobalResourceObject(ResourceClassKey, resourceName, CultureInfo.CurrentUICulture) as string;
            }

            return result;
        }
    }
}
