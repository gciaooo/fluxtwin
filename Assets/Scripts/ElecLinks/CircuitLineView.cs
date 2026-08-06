using UnityEngine;

[RequireComponent(typeof(CircuitLine))]
[RequireComponent(typeof(SpriteRenderer))]
public class CircuitLineView: MonoBehaviour
{
    private CircuitLine circuitLine;
    private LinkRenderer linkRenderer;

    void Start()
    {
        circuitLine = GetComponent<CircuitLine>();
        linkRenderer = gameObject.AddComponent<LinkRenderer>();
        linkRenderer.SetObjectsCoordinates(circuitLine.GetParent(), gameObject);
    }
}