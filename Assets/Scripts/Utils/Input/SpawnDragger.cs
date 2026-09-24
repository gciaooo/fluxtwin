using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnDragger : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject prefabToSpawn;
    private LastElementTracker.ElemType? prefabType;
    [SerializeField] private GridCleaner cleaner;
    private LastElementTracker tracker;

    private GameObject placeholder;
    private Sprite prefabImage;
    
    private Button button; 

    private void Awake()
    {
        prefabImage = prefabToSpawn.GetComponent<SpriteRenderer>().sprite;
        tracker = FindAnyObjectByType<LastElementTracker>();
        prefabType = tracker.FetchElemType(prefabToSpawn);
        button = GetComponent<Button>();
    }

    private void Start()
    {
        tracker.AtLeast += (s, type) => {
            if (type == prefabType-1) button.interactable = true;
        };
        cleaner.OnGridCleaning += (s, empty) =>
        {
            if (prefabType != LastElementTracker.ElemType.ApartmentSource) button.interactable = false;
        };
    }

    private void MakeInteractable()
    {
        button.interactable = true;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!button.interactable) return;

        placeholder = new GameObject(prefabToSpawn.name + "Placeholder");
        SpriteRenderer sr = placeholder.AddComponent<SpriteRenderer>();
        sr.sprite = prefabImage;
        sr.color = Color.white - new Color(0,0,0,0.5f);
        
        placeholder.transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
        placeholder.SetActive(true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!button.interactable) return;

        Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        placeholder.transform.position = newPos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!button.interactable) return;

        Vector3 pos = placeholder.transform.position;
        Vector2 size = placeholder.GetComponent<SpriteRenderer>().size;
        Destroy(placeholder);

        Collider2D col = Physics2D.OverlapBox(pos, size, 0f);
        if (col != null) return;
        
        GameObject prefab = Instantiate(prefabToSpawn, pos, Quaternion.identity);
        prefab.name = tracker.NameElem(prefab, prefabType);
        tracker.OnElementInstantiated(prefab);
    }
}