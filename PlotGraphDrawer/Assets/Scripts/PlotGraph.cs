using System;
using UnityEngine;

[ExecuteInEditMode]
public class PlotGraph : MonoBehaviour
{
    private AnimationCurve curve;
    private float _currentValue;
    public Action OnUpdate;
    
    void Start()
    {
        curve = new AnimationCurve();
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

    private void FixedUpdate()
    {
        if (curve != null)
        {
            curve.AddKey(Time.time, _currentValue);
        }
    }
}
