using UnityEngine;
using UnityEngine.EventSystems;

public class ApplianceController: MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private ApplianceView applianceView;

    [SerializeField]
    private Appliance appliance;

    [SerializeField]
    private WallPlug parentWallPlug;

    void Start()
    {
parentWallPlug.AddAppliance(appliance);
        appliance.OnPowerToggle += (s,isOn) => applianceView.ToggleStatusSprites(isOn);
        appliance.OnPowerDrawChange += (s,powerDraw) => 
        {
applianceView.UpdatePowerDrawView(powerDraw);
parentWallPlug.OnChildPowerDrawChange();
        };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            appliance.TogglePower();   
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            applianceView.SpawnModeSwitcher(appliance);   
        }
    }
}