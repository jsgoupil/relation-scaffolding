using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace RelationScaffolding
{
    public class RelationCollectionHelper
    {
        private ViewDataDictionary _viewDataDictionary;
        private IEnumerable<RelationMemberLookup> _cachedRelationMemberLookup = null;

        public RelationCollectionHelper(ViewDataDictionary viewDataDictionary)
        {
            _viewDataDictionary = viewDataDictionary;
        }

        public IEnumerable<RelationMemberLookup> GetLookup()
        {
            if (_cachedRelationMemberLookup == null)
            {
                if (_viewDataDictionary.Model != null)
                {
                    _cachedRelationMemberLookup = from x in _viewDataDictionary.Model as IEnumerable<object>
                                                  select new RelationMemberLookup(x);
                }
                else
                {
                    _cachedRelationMemberLookup = new RelationMemberLookup[0];
                }
            }

            return _cachedRelationMemberLookup;
        }
    }

}
