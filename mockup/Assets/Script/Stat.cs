using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

[Serializable]
public class Stat {
    [SerializeField]
    private float currentValue = 5f;

    [SerializeField]
    private float maxValue = 5f;
    
    [SerializeField]
    private BarScript bar;

    public float Value
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = Mathf.Clamp(value, 0, MaxValue);

            if (bar != null)
            {
                bar.Value = currentValue;
            }
        }
    }

    public float MaxValue
    {
        get
        {
            return maxValue;
        }
        set
        {
            maxValue = value;
            if (bar != null)
            {
                bar.MaxValue = maxValue;
            }
        }
    }

    // this is used for the very beginning of the game before there is no current
    // just allows value to be read in and set
    public void Initialize(float val, float max)
    {
        MaxValue = max;
        Value = val;
    }

    // used durring game play so that a bar can be attached, then the value for that bar are set
    public void Initialize(BarScript bar, float val, float max)
    {
        this.bar = bar;
        
        this.MaxValue = max;
        this.Value = val;
    }
}
