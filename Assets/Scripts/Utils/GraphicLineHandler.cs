using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class GraphicLineHandler : MonoBehaviour
{
    private CircuitLine circuitLine;
    private SpriteRenderer spriteRenderer;
    private LineRenderer lineRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        circuitLine = GetComponent<CircuitLine>();
        lineRenderer = GetComponent<LineRenderer>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        RenderNewLine();        
    }

    void Update()
    {
        RenderNewLine();
    }

    private void RenderNewLine()
    {
        Vector2 sprRenDims = spriteRenderer.size;
        float startX = sprRenDims.x / 2;
        Vector3 startPos = transform.position - new Vector3(startX, 0, 0);

        lineRenderer.SetPosition(0, circuitLine.ApartmentSourceParent.transform.position);
        lineRenderer.SetPosition(1, startPos);
        lineRenderer.enabled = true;
    }


}
