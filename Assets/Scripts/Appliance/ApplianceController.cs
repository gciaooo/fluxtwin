using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ApplianceController: MonoBehaviour, IPointerClickHandler 
{
    [SerializeField] private ApplianceView applianceView;
    [Space]
    
    [Header("User-Interactable Properties")]
    [Space]
    [SerializeField] public WallPlug parentWallPlug;
    [SerializeField] private Appliance appliance;

    void Start()
    {
        parentWallPlug.AddAppliance(appliance);
        parentWallPlug.OnPowerSurge += (s, empty) => 
        {
            applianceView.RenderPowerSurge();
            DebugLogStats();
        };
        applianceView.SetupLinkRenderer(parentWallPlug);

        appliance.OnPowerToggle += (s,isOn) => applianceView.ToggleStatusSprites(isOn);
        appliance.OnPowerDrawChange += (s,powerDraw) => 
        {
            applianceView.UpdatePowerDrawView(powerDraw);
            parentWallPlug.OnChildPowerDrawChange();
        };
    }
    void Update()
    {
        applianceView.SetupLinkRenderer(parentWallPlug);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            applianceView.SpawnModeSwitcher(appliance);   
        }
    }

    private void DebugLogStats()
    {
        String message = $"Appliance {gameObject.name}\nMode: {appliance.AvailableModes[appliance.CurrentModeIdx].Name}, Power:{appliance.CurrentPowerDraw}W";
        Debug.Log(message);
    }
}