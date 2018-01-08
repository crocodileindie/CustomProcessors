using System;
using System.Collections.Generic;

namespace RTG
{
    public static class Utilities
    {
        internal static bool IsArrayOrList(this Type listType)
        {
            return listType.IsArray || (listType.IsGenericType && listType.GetGenericTypeDefinition() == typeof(List<>));
        }

        internal static bool IsList(this Type listType)
        {
            return (listType.IsGenericType && listType.GetGenericTypeDefinition() == typeof(List<>));
        }

        internal static Type GetArrayOrListElementType(this Type listType)
        {
            Type result;
            if (listType.IsArray)
            {
                result = listType.GetElementType();
            }
            else if (listType.IsGenericType && listType.GetGenericTypeDefinition() == typeof(List<>))
            {
                result = listType.GetGenericArguments()[0];
            }
            else
            {
                result = null;
            }
            return result;
        }
    }
}