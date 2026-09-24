using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ApplianceView : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;
    private SpriteRenderer sr;
    
    private DragDropper dragDropper;
    private LinkRenderer linkRenderer;

    private bool isFirstSpawn = true;

    private ApplianceModeSwitcher modeSwitcher;
    [SerializeField]
    private ApplianceModeSwitcher modeSwitcherPrefab;
    
    [SerializeField]
    private ModeSwitcherHandler modeSwitcherHandler;

    public void ToggleStatusSprites(bool isOn)
    {
        sr.sprite = isOn ? spriteOn : spriteOff;

        modeSwitcher.ToggleStatusButtonSprite(isOn);
    }

    public void SpawnModeSwitcher(Appliance appliance)
    {
        if (isFirstSpawn) {
            modeSwitcher.SetSwitcherData(gameObject.name, appliance);
            isFirstSpawn = false;
        }
        modeSwitcher.gameObject.SetActive(true);
        modeSwitcherHandler.OnModeSwitcherActivated(modeSwitcher);        
    }

    public void DestroyModeSwitcher()
    {
        Destroy(modeSwitcher.gameObject);
    }

    public void UpdatePowerDrawView(double powerDraw)
    {
        modeSwitcher.UpdatePowerDraw(powerDraw);
    }

    public void ChangeAlpha(float alpha)
    {
        sr.color = Color.white - new Color(0,0,0,alpha);
    }

    public void SetupLinkRenderer(WallPlug parent)
    {
       linkRenderer.SetObjectsCoordinates(parent.gameObject, gameObject); 
    }

    public void RenderPowerSurge()
    {
        linkRenderer.SetErrorLine();
    }

    void Awake()
    {
        dragDropper = gameObject.AddComponent<DragDropper>();
        linkRenderer = gameObject.AddComponent<LinkRenderer>();        
        modeSwitcherHandler = FindAnyObjectByType<ModeSwitcherHandler>();
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        modeSwitcher = Instantiate(modeSwitcherPrefab);
        modeSwitcher.gameObject.SetActive(false);
    }
}