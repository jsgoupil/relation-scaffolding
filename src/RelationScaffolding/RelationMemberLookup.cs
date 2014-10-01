using System;
using System.Linq;
using System.Reflection;

namespace RelationScaffolding
{
    public class RelationMemberLookup
    {
        private MemberInfo[] _cachedMembers = null;
        private MemberInfo _cachedKeyMemberInfo = null;
        private MemberInfo _cachedDisplayMemberInfo = null;
        private bool _selectedObjectKeyComputed = false;
        private object _cachedSelectedObjectKey = null;
        private bool _selectedObjectDisplayComputed = false;
        private object _cachedSelectedObjectDisplay = null;
        private Type _type;
        private object _obj;

        public RelationMemberLookup(object obj)
            : this(obj, obj != null ? obj.GetType() : typeof(object))
        {
        }

        public RelationMemberLookup(object obj, Type type)
        {
            _obj = obj;
            _type = type;
        }

        public MemberInfo KeyMemberInfo
        {
            get
            {
                if (_cachedKeyMemberInfo == null)
                {
                    _cachedKeyMemberInfo = Members.FirstOrDefault(m => m.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) != null);
                    if (_cachedKeyMemberInfo == null)
                    {
                        // Let's try to find something that has the word Id
                        _cachedKeyMemberInfo = Members.FirstOrDefault(m => m.MemberType != MemberTypes.Method && m.Name.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase));
                        if (_cachedKeyMemberInfo == null)
                        {
                            throw new Exception("We couldn't find the key for your model. " + _type);
                        }
                    }
                }

                return _cachedKeyMemberInfo;
            }
        }

        public MemberInfo DisplayMemberInfo
        {
            get
            {
                if (_cachedDisplayMemberInfo == null)
                {
                    _cachedDisplayMemberInfo = Members.FirstOrDefault(m => m.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(RelationDisplayAttribute)) != null);
                    _cachedDisplayMemberInfo = _cachedDisplayMemberInfo ?? KeyMemberInfo;
                }

                return _cachedDisplayMemberInfo;
            }
        }

        public object SelectedObjectKey
        {
            get
            {
                if (_selectedObjectKeyComputed == false)
                {
                    if (_obj != null)
                    {
                        _cachedSelectedObjectKey = ((System.Reflection.PropertyInfo)KeyMemberInfo).GetValue(_obj);
                    }

                    _selectedObjectKeyComputed = true;
                }

                return _cachedSelectedObjectKey;
            }
        }

        public object SelectedObjectDisplay
        {
            get
            {
                if (_selectedObjectDisplayComputed == false)
                {
                    if (_obj != null)
                    {
                        _cachedSelectedObjectDisplay = ((System.Reflection.PropertyInfo)DisplayMemberInfo).GetValue(_obj);
                    }

                    _selectedObjectDisplayComputed = true;
                }

                return _cachedSelectedObjectDisplay;
            }
        }

        private MemberInfo[] Members
        {
            get
            {
                return _cachedMembers ?? (_cachedMembers = _type.GetMembers(BindingFlags.Public | BindingFlags.Instance));
            }
        }
    }


}
