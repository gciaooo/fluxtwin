using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public event EventHandler<double> OnPowerDrawChange;

    private bool isPoweredOn = false;


    public void TogglePower()
    {
        isPoweredOn = !isPoweredOn;

        if (!isPoweredOn) SetPowerDraw(0);
        else SetPowerDraw(availableModes[CurrentModeIdx].PowerDraw);
    
        OnPowerToggle?.Invoke(this, isPoweredOn);
    }
    
    private void SetPowerDraw(double power)
    {
        CurrentPowerDraw = power;
        OnPowerDrawChange?.Invoke(this, CurrentPowerDraw);
    }

    public void SwitchMode(int modeIdx)
    {
        if (modeIdx >= availableModes.Count || modeIdx == CurrentModeIdx) return;
        CurrentModeIdx = modeIdx;
        Debug.Log("Current Mode = " + CurrentModeIdx);
        if (!isPoweredOn) return;
        SetPowerDraw(availableModes[CurrentModeIdx].PowerDraw);
        Debug.Log("Current PowerDraw = " + CurrentPowerDraw);
    }
}
