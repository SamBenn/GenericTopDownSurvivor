using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReflectionUtility
{
    public static T ReflectPropertyFromObject<T>(Type type, string propertyName, object obj = null)
    {
        var toReturn = default(T);

        if (string.IsNullOrEmpty(propertyName))
            return toReturn;

        var property = type.GetProperty(propertyName);

        if (property != null)
            toReturn = (T)property.GetValue(obj);

        return toReturn;
    }
}
