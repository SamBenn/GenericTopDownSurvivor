using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ReflectionUtility
{
    public static T ReflectPropertyFromObject<T>(Type T2, string propertyName, object obj = null)
    {
        var toReturn = default(T);

        if (string.IsNullOrEmpty(propertyName))
            return toReturn;

        var property = T2.GetProperty(propertyName);

        toReturn = (T)property.GetValue(obj, null);

        return toReturn;
    }
}
