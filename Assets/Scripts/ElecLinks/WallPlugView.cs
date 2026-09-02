using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(WallPlug))]
public class WallPlugView : MonoBehaviour
{
    private WallPlug wallPlug;
    private LinkRenderer linkRenderer;
    private DragDropper dragDropper;
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

        dragDropper = gameObject.AddComponent<DragDropper>();

        linkRenderer = gameObject.AddComponent<LinkRenderer>();
        linkRenderer.SetObjectsCoordinates(wallPlug.GetParent(), gameObject);

        SetPowerDrawText(0);
    }

    void Update()
    {
        linkRenderer.SetObjectsCoordinates(wallPlug.GetParent(), gameObject);
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