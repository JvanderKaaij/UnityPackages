using System;
using UnityEngine;

[ExecuteInEditMode]
public class PlotGraph : MonoBehaviour
{
    private AnimationCurve curve;
    private float _currentValue;
    public Action OnUpdate;
    private float lastClearTime;
    void Start()
    {
        curve = new AnimationCurve();
        lastClearTime = Time.time;
    }

    public AnimationCurve GetCurve()
    {
        return curve;
    }

    public void WriteData(float value)
    {
        _currentValue = value;
        OnUpdate?.Invoke();
    }
    
    public void Clear()
    {
        curve = new AnimationCurve();
        lastClearTime = Time.time;
    }

    private void FixedUpdate()
    {
        if (curve != null)
        {
            curve.AddKey(Time.time - lastClearTime, _currentValue);
        }
    }
}
