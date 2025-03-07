using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Condition
{
    public Condition(float max, Action<float> onchanged)
    {
        maxValue = max;
        curValue = maxValue;
        onconditionChanged += onchanged;
        onconditionChanged?.Invoke(GetPercent());
    }
    public Condition(float max, float cur, Action<float> onchanged)
    {
        maxValue = max;
        curValue = cur;
        onconditionChanged += onchanged;
        onconditionChanged?.Invoke(GetPercent());
    }

    public float maxValue;
    public float curValue;

    public event Action<float> onconditionChanged;
    public float GetPercent()
    {
        return curValue / maxValue;
    }

    public float Add(float amount)
    {
        curValue += amount;
        if(curValue >= maxValue)
        {
            curValue = maxValue;
        }
        onconditionChanged?.Invoke(GetPercent());
        return curValue;
    }

    public float Subtract(float amount)
    {
        curValue -= amount;
        if(curValue < 0)
        {
            curValue = 0;
        }
        onconditionChanged?.Invoke(GetPercent());
        return curValue;
    }
}
