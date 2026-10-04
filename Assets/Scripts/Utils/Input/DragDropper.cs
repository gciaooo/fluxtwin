using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
//Component class that enables dragging of elements in the electrical grid when added as a Component
public class DragDropper: MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void ChangeAlpha(float alpha)
    {
        sr.color = Color.white - new Color(0,0,0,alpha);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        ChangeAlpha(0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        ChangeAlpha(0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        transform.position = newPos;
    }
}