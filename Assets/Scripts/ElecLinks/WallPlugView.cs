using System;
using UnityEngine;

[RequireComponent(typeof(WallPlug))]
public class WallPlugView : MonoBehaviour
{
    private WallPlug wallPlug;
    private LinkRenderer linkRenderer;

    void Start()
    {
        wallPlug = GetComponent<WallPlug>();
        wallPlug.OnPowerSurge += (s, total) => DebugLogStats(total);
        wallPlug.CircuitLineParent.OnPowerSurge += (s, total) 
        => linkRenderer.SetErrorLine();
        
        linkRenderer = gameObject.AddComponent<LinkRenderer>();
        linkRenderer.SetObjectsCoordinates(wallPlug.GetParent(), gameObject);
    }
    public void DebugLogStats(double total)
    {
        String message = $"WallPlug {gameObject.name}\nPower: {total}W, Limit: {wallPlug.MaximumPowerDraw}";
        if (total <= wallPlug.MaximumPowerDraw) Debug.Log(message);
        else Debug.LogWarning(message);
    }
};