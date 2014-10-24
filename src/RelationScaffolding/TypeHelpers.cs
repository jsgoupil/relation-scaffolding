using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace RelationScaffolding
{
    public class TypeHelpers
    {
        public static Type ExtractGenericInterface(Type queryType, Type interfaceType)
        {
            if (MatchesGenericType(queryType, interfaceType))
            {
                return queryType;
            }
            Type[] queryTypeInterfaces = queryType.GetInterfaces();
            return MatchGenericTypeFirstOrDefault(queryTypeInterfaces, interfaceType);
        }

        private static bool MatchesGenericType(Type type, Type matchType)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == matchType;
        }

        private static Type MatchGenericTypeFirstOrDefault(Type[] types, Type matchType)
        {
            for (int i = 0; i < types.Length; i++)
            {
                Type type = types[i];
                if (MatchesGenericType(type, matchType))
                {
                    return type;
                }
            }
            return null;
        }
    }
}
