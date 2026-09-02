using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(WallPlug))]
public class WallPlugView : MonoBehaviour
{
    private WallPlug wallPlug;
    private LinkRenderer linkRenderer;
    [SerializeField]
    private TextMeshPro powerText;

    void Start()
    {
        wallPlug = GetComponent<WallPlug>();
        wallPlug.OnPowerSurge += (s, total) => DebugLogStats(total);
        wallPlug.CircuitLineParent.OnPowerSurge += (s, total) 
        => linkRenderer.SetErrorLine();
        
        wallPlug.OnTotalPowerDrawChange += (s, total)
        => SetPowerDrawText(total);

        linkRenderer = gameObject.AddComponent<LinkRenderer>();
        linkRenderer.SetObjectsCoordinates(wallPlug.GetParent(), gameObject);

        SetPowerDrawText(0);
    }

    private void SetPowerDrawText(double total)
    {
        powerText.text = $"{total} / {wallPlug.MaximumPowerDraw}";         
    }

    public void DebugLogStats(double total)
    {
        String message = $"WallPlug {gameObject.name}\nPower: {total}W, Limit: {wallPlug.MaximumPowerDraw}";
        if (total <= wallPlug.MaximumPowerDraw) Debug.Log(message);
        else Debug.LogWarning(message);
    }
};