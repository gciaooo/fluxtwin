using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] GameObject prefabToSpawn;
    private LastElementTracker tracker;

    private GameObject placeholder;
    private Sprite prefabImage;

    private void Awake()
    {
        prefabImage = prefabToSpawn.GetComponent<SpriteRenderer>().sprite;
        tracker = FindAnyObjectByType<LastElementTracker>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        placeholder = new GameObject(prefabToSpawn.name + "Placeholder");
        SpriteRenderer sr = placeholder.AddComponent<SpriteRenderer>();
        sr.sprite = prefabImage;
        sr.color = Color.white - new Color(0,0,0,0.5f);
        
        placeholder.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
        placeholder.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        placeholder.transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = placeholder.transform.position;
        Vector2 size = placeholder.GetComponent<SpriteRenderer>().size;
        Destroy(placeholder);

        Collider2D col = Physics2D.OverlapBox(pos, size, 0f);
        if (col != null) return;
        
        GameObject prefab = Instantiate(prefabToSpawn, pos, Quaternion.identity);
        prefab.name = tracker.nameElem(prefab);
        tracker.OnElementInstantiated(prefab);
    }
}