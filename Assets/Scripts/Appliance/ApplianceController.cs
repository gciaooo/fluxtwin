using UnityEngine;
using UnityEngine.EventSystems;

public class ApplianceController: MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ApplianceView applianceView;

    [SerializeField]
    private Appliance appliance;

    void Start()
    {
        appliance.OnPowerToggle += (s,isOn) => applianceView.ToggleSprite(isOn);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            appliance.TogglePower();
        if (eventData.button == PointerEventData.InputButton.Right)
            applianceView.SpawnModeSwitcher(appliance);
    }
}