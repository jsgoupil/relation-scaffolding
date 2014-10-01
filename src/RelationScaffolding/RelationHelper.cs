using System.Web.Mvc;

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
    }
}
