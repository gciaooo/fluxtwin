using UnityEngine;

//Class that shows links between a grid element and its parent. Uses a LineRenderer component for the graphic representation of the link. LinkRenderers are handled by objects that are children of the link. For example, in the link between a CircuitLineView and a WallPlugView, the reference of the LinkRenderer is kept on WallPlugView.
public class LinkRenderer : MonoBehaviour
{
    private Vector3 parentPos;
    private Vector3 childPos;
    private Color defaultColor = new(0.39f, 0.6f, 1);    
    private Color errorColor = new(0.85882352f, 0.16862745098039217f, 0.2235294117647059f);
    private bool isReady = false;

    private LineRenderer lineRenderer;

    public void SetObjectsCoordinates(GameObject parent, GameObject child)
    {
        parentPos = parent.transform.position;
        childPos = child.transform.position;
        isReady = true;
    }

    public void SetErrorLine()
    {
        lineRenderer.startColor = errorColor;
        lineRenderer.endColor = errorColor;
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
        Vector3 parentLinePos = parentPos;
        Vector3 componentLinePos = childPos;
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
