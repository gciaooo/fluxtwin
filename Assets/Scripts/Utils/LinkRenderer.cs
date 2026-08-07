using UnityEngine;

public class LinkRenderer : MonoBehaviour
{
    private Vector3 parentPos;
    private Vector3 parentDims;

    private Vector3 childPos;
    private Vector3 childDims;
    private Color defaultColor = new(0.39f, 0.6f, 1);    
    private bool isReady = false;

    private LineRenderer lineRenderer;

    public void SetObjectsCoordinates(GameObject parent, GameObject child)
    {
        SpriteRenderer parentSr = parent.GetComponent<SpriteRenderer>();
        SpriteRenderer componentSr = child.GetComponent<SpriteRenderer>();
        if (parentSr == null || componentSr == null)
        {
            Debug.LogError("SpriteRenderer for LinkRenderer is null");
        }
        parentPos = parent.transform.position;
        parentDims = parentSr.size;
        childPos = child.transform.position;
        childDims = componentSr.size;
        isReady = true;
    }

    public void SetErrorLine()
    {
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        isReady = true;
    }

    private void SetDefaults()
    {
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = defaultColor;
        lineRenderer.endColor = defaultColor;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
    }

    private void SetLineVertices()
    {
        float startX = childDims.x / 2;
        Vector3 parentLinePos = parentPos;
        Vector3 componentLinePos = childPos - new Vector3(startX, 0, 0);
        parentLinePos.z = 1;
        componentLinePos.z = 1;

        lineRenderer.SetPosition(0, parentLinePos);
        lineRenderer.SetPosition(1, componentLinePos);
        lineRenderer.enabled = true;
    }

    void Awake()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    void Start()
    {
        SetDefaults();
    }

    void Update()
    {
        if(isReady)
        {
            SetLineVertices();
            isReady = false;
        }
    }
}
