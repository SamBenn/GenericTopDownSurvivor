using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumUtility
{
    public static T ParseForTag<T>(string toParse, T defaultOfEnum = default)
        where T : struct, System.Enum
    {
        if (string.IsNullOrEmpty(toParse))
            return defaultOfEnum;

        return Enum.Parse<T>(toParse);
    }
}
