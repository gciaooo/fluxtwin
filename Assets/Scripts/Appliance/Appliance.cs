using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Appliance
{
    [SerializeField]
    private List<ApplianceMode> availableModes;
    public int CurrentMode {get; set;} = -1;
    public double CurrentPowerDraw {get; set;} = 0;
    public event EventHandler<bool> OnPowerToggle;

    private bool isPoweredOn = false;


    public void TogglePower()
    {
        isPoweredOn = !isPoweredOn;
        if (!isPoweredOn) CurrentPowerDraw = 0;
        OnPowerToggle?.Invoke(this, isPoweredOn);
    }

    public void SwitchMode(int mode)
    {
        if (availableModes.Count >= mode) return;
        CurrentMode = mode;
        if (!isPoweredOn) return;
        CurrentPowerDraw = availableModes[CurrentMode].PowerDraw;  
    }
}
