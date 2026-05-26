using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ApplianceView : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;
    private SpriteRenderer sr;

    [SerializeField]
    private Canvas modeSwitcherCanvas;
    private ApplianceModeSwitcher modeSwitcher;
    [SerializeField]
    private ApplianceModeSwitcher modeSwitcherPrefab;

    public void ToggleSprite(bool isOn)
    {
        sr.sprite = isOn ? spriteOn : spriteOff;
    }

    public void SpawnModeSwitcher(Appliance appliance)
    {
        if (modeSwitcher == null)
        {   
            modeSwitcher = Instantiate(modeSwitcherPrefab, modeSwitcherCanvas.transform);
            modeSwitcher.SetSwitcherData(gameObject.name, appliance);
        }
        else
        {
            modeSwitcher.gameObject.SetActive(true);
        }
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
}