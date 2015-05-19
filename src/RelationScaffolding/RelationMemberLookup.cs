using System;
using System.Linq;
using System.Reflection;

namespace RelationScaffolding
{
    public class RelationMemberLookup
    {
        private MemberInfo[] _cachedMembers = null;
        private MemberData _cachedKeyMember = null;
        private MemberData _cachedDisplayMember = null;
        private MemberData _cachedEditMember = null;
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

        public MemberData KeyMember
        {
            get
            {
                if (_cachedKeyMember == null)
                {
                    var memberInfo = Members.FirstOrDefault(m => m.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)) != null);
                    if (memberInfo == null)
                    {
                        // Let's try to find something that has the word Id
                        memberInfo = Members.FirstOrDefault(m => m.MemberType != MemberTypes.Method && m.Name.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase));
                        if (memberInfo == null)
                        {
                            throw new Exception("We couldn't find the key for your model. " + _type);
                        }
                    }

                    _cachedKeyMember = new MemberData(memberInfo, _obj);
                }

                return _cachedKeyMember;
            }
        }

        public MemberData DisplayMember
        {
            get
            {
                if (_cachedDisplayMember == null)
                {
                    var lookup = this;
                    while (lookup != null)
                    {
                        var memberInfo = lookup.Members.FirstOrDefault(m => m.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(RelationDisplayAttribute)) != null);
                        if (memberInfo == null)
                        {
                            break;
                        }
                        
                        var propertyInfo = memberInfo as PropertyInfo;

                        _cachedDisplayMember = new MemberData(memberInfo, lookup._obj);
                        var value = propertyInfo.GetValue(lookup._obj);
                        lookup = new RelationMemberLookup(value, propertyInfo.PropertyType);
                    }

                    _cachedDisplayMember = _cachedDisplayMember ?? KeyMember;
                }

                return _cachedDisplayMember;
            }
        }

        public MemberData EditMember
        {
            get
            {
                if (_cachedEditMember == null)
                {
                    var memberInfo = Members.FirstOrDefault(m => m.CustomAttributes.FirstOrDefault(c => c.AttributeType == typeof(RelationEditAttribute)) != null);
                    _cachedEditMember = new MemberData(memberInfo, _obj);
                }

                return _cachedEditMember;
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

    public class MemberData
    {
        private MemberInfo _memberInfo;
        private object _obj;
        private object _cachedValue;
        private bool _computedValue;

        public MemberData(MemberInfo memberInfo, object obj)
        {
            _memberInfo = memberInfo;
            _obj = obj;
            _cachedValue = null;
        }
        public MemberInfo MemberInfo
        {
            get
            {
                return _memberInfo;
            }
        }

        public object Value
        {
            get
            {
                if (_obj != null && _computedValue == false)
                {
                    _cachedValue = (MemberInfo as PropertyInfo).GetValue(_obj);
                    _computedValue = true;
                }

                return _cachedValue;
            }
        }
    }


}
