using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorUtil
{
    public static Vector2 GetRandomLocationFromTarget(Vector2 target, float distance)
    {
        var angle = (double)UnityEngine.Random.Range(0, 360);
        var dirVector = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        var toReturn = target + dirVector * distance;
        return toReturn;
    }
}
