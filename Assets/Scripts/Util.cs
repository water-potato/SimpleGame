using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{ 
    public static float SinLerp(float t)
    {
        return
        t = Mathf.Sin(t * Mathf.PI * 0.5f);
    }

    public static float SmoothIncrease(float t)
    {
        return t * t * (3f - 2f * t);
    }
}
