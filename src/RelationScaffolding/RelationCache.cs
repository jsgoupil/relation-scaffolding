using System;
using System.Collections.Generic;
using System.Web;

namespace RelationScaffolding
{
    class RelationCache
    {
        public const string KEY = "RelationCache";
        private HttpContextBase _httpContextBase;

        public RelationCache(HttpContextBase httpContextBase)
        {
            _httpContextBase = httpContextBase;
        }

        public void Save(Type type, object id, object value)
        {
            var dict = GetDict();
            IDictionary<object, object> typeDict = null;
            if (!dict.TryGetValue(type, out typeDict))
            {
                dict[type] = typeDict = new Dictionary<object, object>();
            }

            typeDict[id] = value;
        }

        public object Retrieve(Type type, object id)
        {
            IDictionary<object, object> value = null;
            if (GetDict().TryGetValue(type, out value))
            {
                object finalValue = null;
                value.TryGetValue(id, out finalValue);
                return finalValue;
            }

            return null;
        }

        private IDictionary<Type, IDictionary<object, object>> GetDict()
        {
            if (_httpContextBase.Items[KEY] == null)
            {
                _httpContextBase.Items[KEY] = new Dictionary<Type, IDictionary<object, object>>();
            }

            return (IDictionary<Type, IDictionary<object, object>>)_httpContextBase.Items[KEY];
        }
    }
}
