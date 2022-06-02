using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Interpolation
{
    public static IEnumerator Interpolate(float targetTime, float startValue, float endValue, Action<float> action)
    {
        float lerpTime = 0f;

        while(lerpTime < targetTime)
        {
            lerpTime+= Time.deltaTime;

            float percentage = lerpTime / targetTime;
            float finalValue = Mathf.Lerp(startValue, endValue, percentage);

            if (action != null)
                action.Invoke(finalValue);

            yield return null;
        }
    }
}
