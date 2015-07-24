using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace RelationScaffolding
{
    public class RelationMemberLookup
    {
        private MemberInfo[] _cachedMembers = null;
        private MemberInfo[] _cachedMetadataMembers = null;
        private bool _metadataMembersComputed = false;

        private MemberData _cachedKeyMember = null;
        private MemberData _cachedDisplayMember = null;
        private MemberData _cachedEditMember = null;
        private IEnumerable<MemberData> _cachedListMembers = null;
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
                    var memberData = GetMemberDataFromCustomAttribute(this, typeof(System.ComponentModel.DataAnnotations.KeyAttribute), _obj);
                    if (memberData == null)
                    {
                        // Let's try to find something that has the word Id
                        var memberInfo = Members.FirstOrDefault(m => m.MemberType != MemberTypes.Method && m.Name.EndsWith("Id", StringComparison.InvariantCultureIgnoreCase));
                        if (memberInfo == null)
                        {
                            throw new Exception("We couldn't find the key for your model. " + _type);
                        }

                        _cachedKeyMember = new MemberData(memberInfo, null, _obj);
                    }
                    else
                    {
                        _cachedKeyMember = memberData;
                    }
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
                        var memberData = GetMemberDataFromCustomAttribute(lookup, typeof(RelationDisplayAttribute), lookup._obj);
                        if (memberData == null)
                        {
                            break;
                        }

                        _cachedDisplayMember = memberData;

                        if (lookup._obj != null)
                        {
                            var value = memberData.Value;
                            lookup = new RelationMemberLookup(value, (memberData.MemberInfo as PropertyInfo).PropertyType);
                        }
                        else
                        {
                            lookup = null;
                        }
                    }

                    _cachedDisplayMember = _cachedDisplayMember ?? KeyMember;
                }

                return _cachedDisplayMember;
            }
        }

        public IEnumerable<MemberData> List
        {
            get
            {
                if (_cachedListMembers == null)
                {
                    var memberData = GetMemberDatasFromCustomAttribute(this, typeof(RelationListAttribute), _obj);
                    _cachedListMembers = memberData;
                }

                return _cachedListMembers;
            }
        }

        public MemberData EditMember
        {
            get
            {
                if (_cachedEditMember == null)
                {
                    var memberData = GetMemberDataFromCustomAttribute(this, typeof(RelationEditAttribute), _obj);
                    _cachedEditMember = memberData;
                }

                return _cachedEditMember;
            }
        }

        private MemberData GetMemberDataFromCustomAttribute(RelationMemberLookup lookup, Type attribute, object obj)
        {
            return GetMemberDatasFromCustomAttribute(lookup, attribute, obj).FirstOrDefault();
        }

        private IEnumerable<MemberData> GetMemberDatasFromCustomAttribute(RelationMemberLookup lookup, Type attribute, object obj)
        {
            var memberInfos = lookup.Members.Where(m => m.CustomAttributes.FirstOrDefault(c => c.AttributeType == attribute) != null);
            if (memberInfos.Count() == 0)
            {
                if (lookup.MetadataMembers != null)
                {
                    memberInfos = lookup.MetadataMembers.Where(m => m.CustomAttributes.FirstOrDefault(c => c.AttributeType == attribute) != null);
                    if (memberInfos.Count() > 0)
                    {
                        return memberInfos.Select(m =>
                        {
                            var memberInfo = lookup.Members.FirstOrDefault(x => x.Name == m.Name);
                            return new MemberData(memberInfo, m, obj);
                        });
                    }
                }

                return new MemberData[0];
            }

            return memberInfos.Select(m => new MemberData(m, null, obj));
        }

        private MemberInfo[] Members
        {
            get
            {
                return _cachedMembers ?? (_cachedMembers = _type.GetMembers(BindingFlags.Public | BindingFlags.Instance));
            }
        }

        private MemberInfo[] MetadataMembers
        {
            get
            {
                if (!_metadataMembersComputed)
                {
                    var metadataType = _type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType<MetadataTypeAttribute>().FirstOrDefault();
                    if (metadataType != null)
                    {
                        _cachedMetadataMembers = metadataType.MetadataClassType.GetMembers(BindingFlags.Public | BindingFlags.Instance);
                    }

                    _metadataMembersComputed = true;
                }

                return _cachedMetadataMembers;
            }
        }
    }

    public class MemberData
    {
        private MemberInfo _memberInfo;
        private MemberInfo _metadataMemberInfo;
        private object _obj;
        private object _cachedValue;
        private bool _computedValue;

        public MemberData(MemberInfo memberInfo, MemberInfo metadataMemberInfo, object obj)
        {
            _memberInfo = memberInfo;
            _metadataMemberInfo = metadataMemberInfo;
            _obj = obj;
            _cachedValue = null;
        }

        public MemberInfo MemberInfo
        {
            get
            {
                return _metadataMemberInfo ?? _memberInfo;
            }
        }

        public object Value
        {
            get
            {
                if (_obj != null && _computedValue == false)
                {
                    _cachedValue = (_memberInfo as PropertyInfo).GetValue(_obj);
                    _computedValue = true;
                }

                return _cachedValue;
            }
        }
    }


}
