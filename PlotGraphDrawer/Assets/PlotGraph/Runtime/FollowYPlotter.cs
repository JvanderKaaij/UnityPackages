using System;
using UnityEngine;

[RequireComponent(typeof(PlotGraph))]
public class FollowYPlotter : MonoBehaviour
{
    private PlotGraph plot;

    private void Start()
    {
        plot = GetComponent<PlotGraph>();
    }

    private void FixedUpdate()
    {
        plot.WriteData(transform.position.y);
    }
}
