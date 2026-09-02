using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(CircuitLine))]
[RequireComponent(typeof(SpriteRenderer))]
public class CircuitLineView : MonoBehaviour
{
    private CircuitLine circuitLine;
    private DragDropper dragDropper;
    private LinkRenderer linkRenderer;
    [SerializeField]
    private TextMeshPro powerText;

    void Start()
    {
        circuitLine = GetComponent<CircuitLine>();
        circuitLine.OnPowerSurge += (s, total) => DebugLogStats(total);
        circuitLine.ApartmentSourceParent.OnPowerSurge += (s, total)
        => linkRenderer.SetErrorLine();

        circuitLine.OnTotalPowerDrawChange += (s, total)
        => SetPowerDrawText(total);

        dragDropper = gameObject.AddComponent<DragDropper>();

        linkRenderer = gameObject.AddComponent<LinkRenderer>();
        linkRenderer.SetObjectsCoordinates(circuitLine.GetParent(), gameObject);

        SetPowerDrawText(0);
    }

    void Update()
    {
        linkRenderer.SetObjectsCoordinates(circuitLine.GetParent(), gameObject);
    }

    private void SetPowerDrawText(double total)
    {
        powerText.text = $"{total} / {circuitLine.MaximumPowerDraw}";         
    }

    public void DebugLogStats(double total)
    {
        String message = $"CircuitLine {gameObject.name}\nPower: {total}W, Limit: {circuitLine.MaximumPowerDraw}";
        if (total <= circuitLine.MaximumPowerDraw) Debug.Log(message);
        else Debug.LogWarning(message);
    }
}