using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Linq;

namespace RelationScaffolding
{
    public class RelationHelper
    {
        private ViewDataDictionary _viewDataDictionary;
        private RelationMemberLookup _cachedRelationMemberLookup = null;

        public RelationHelper(ViewDataDictionary viewDataDictionary)
        {
            _viewDataDictionary = viewDataDictionary;
        }

        public RelationMemberLookup GetLookup()
        {
            if (_cachedRelationMemberLookup == null)
            {
                _cachedRelationMemberLookup = new RelationMemberLookup(_viewDataDictionary.Model, _viewDataDictionary.ModelMetadata.ModelType);
            }

            return _cachedRelationMemberLookup;
        }

        public static TResult GetCustomAttributesFromContainer<TResult>(Type container, string memberName)
        {
            var customAttribute = container.GetMembers().FirstOrDefault(x => x.Name == memberName).GetCustomAttributes(typeof(TResult), true).OfType<TResult>().FirstOrDefault();

            if (customAttribute == null)
            {
                // Check the metadata type
                var metadataType = container.GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType<MetadataTypeAttribute>().FirstOrDefault();
                if (metadataType != null)
                {
                    return GetCustomAttributesFromContainer<TResult>(metadataType.MetadataClassType, memberName);
                }
            }

            return customAttribute;
        }
    }
}
