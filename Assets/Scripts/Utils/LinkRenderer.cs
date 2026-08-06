using UnityEngine;

public class LinkRenderer : MonoBehaviour
{
    private Vector3 parentPos;
    private Vector3 parentDims;

    private Vector3 componentPos;
    private Vector3 componentDims;

    private bool isReady = false;

    private LineRenderer lineRenderer;

    public void SetObjectsCoordinates(GameObject parent, GameObject component)
    {
        SpriteRenderer parentSr = parent.GetComponent<SpriteRenderer>();
        SpriteRenderer componentSr = GetComponent<SpriteRenderer>();
        if (parentSr == null || componentSr == null)
        {
            Debug.LogError("SpriteRenderer for LinkRenderer is null");
        }
        parentPos = parent.transform.position;
        parentDims = parentSr.size;
        componentPos = component.transform.position;
        componentDims = componentSr.size;
        isReady = true;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    void Update()
    {
        if(isReady)
        {
            SetLineVertices();
            isReady = false;
        }
    }

    private void SetLineVertices()
    {
        float startX = componentDims.x / 2;
        Vector3 parentLinePos = parentPos;
        Vector3 componentLinePos = componentPos - new Vector3(startX, 0, 0);

        lineRenderer.SetPosition(0, parentLinePos);
        lineRenderer.SetPosition(1, componentLinePos);
        lineRenderer.enabled = true;
    }
}
