using System;
using UnityEngine;

[RequireComponent(typeof(CircuitLine))]
[RequireComponent(typeof(SpriteRenderer))]
public class CircuitLineView : MonoBehaviour
{
    private CircuitLine circuitLine;
    private LinkRenderer linkRenderer;

    void Start()
    {
        circuitLine = GetComponent<CircuitLine>();
        circuitLine.OnPowerSurge += (s, total) => DebugLogStats(total);
        circuitLine.ApartmentSourceParent.OnPowerSurge += (s, total)
        => linkRenderer.SetErrorLine();

        linkRenderer = gameObject.AddComponent<LinkRenderer>();
        linkRenderer.SetObjectsCoordinates(circuitLine.GetParent(), gameObject);
    }
    public void DebugLogStats(double total)
    {
        String message = $"CircuitLine {gameObject.name}\nPower: {total}W, Limit: {circuitLine.MaximumPowerDraw}";
        if (total <= circuitLine.MaximumPowerDraw) Debug.Log(message);
        else Debug.LogWarning(message);
    }
}