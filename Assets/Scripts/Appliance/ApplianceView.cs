using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ApplianceView : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;
    private SpriteRenderer sr;

    private bool isFirstSpawn = true;

    private ApplianceModeSwitcher modeSwitcher;
    [SerializeField]
    private ApplianceModeSwitcher modeSwitcherPrefab;

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
    }

    public void UpdatePowerDrawView(double powerDraw)
    {
        modeSwitcher.UpdatePowerDraw(powerDraw);
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        modeSwitcher = Instantiate(modeSwitcherPrefab);
        modeSwitcher.gameObject.SetActive(false);
    }
}