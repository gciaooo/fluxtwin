using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Appliance
{
    [SerializeField]
    private List<ApplianceMode> availableModes;
    public ReadOnlyCollection<ApplianceMode> AvailableModes {get => availableModes.AsReadOnly();}

    public int CurrentModeIdx {get; set;} = 0;
    public double CurrentPowerDraw {get; set;} = 0;
    public event EventHandler<bool> OnPowerToggle;

    private bool isPoweredOn = false;


    public void TogglePower()
    {
        isPoweredOn = !isPoweredOn;
        if (!isPoweredOn) CurrentPowerDraw = 0;
        OnPowerToggle?.Invoke(this, isPoweredOn);
    }

    public void SwitchMode(int modeIdx)
    {
        if (modeIdx >= availableModes.Count || modeIdx == CurrentModeIdx) return;
        CurrentModeIdx = modeIdx;
        Debug.Log("Current Mode = " + CurrentModeIdx);
        if (!isPoweredOn) return;
        CurrentPowerDraw = availableModes[CurrentModeIdx].PowerDraw;  
        Debug.Log("Current PowerDraw = " + CurrentPowerDraw);
    }
}
