using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

[RequireComponent(typeof(ApplianceController))]
[RequireComponent(typeof(ApplianceView))]
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
    public ApplianceTimer Timer = new();

    private bool isPoweredOn = false;
    private bool isTimerInitialized = false;

    Appliance()
    {
        Timer.CompletedEvent += () => OnTimerCompleted();
    }

    public void OnTimerCompleted()
    {
        Timer.Stop();
        isTimerInitialized = false;
        TogglePower();
    }

    public void TogglePower()
    {
        isPoweredOn = !isPoweredOn;

        if (!isPoweredOn) SetPowerDraw(0);
        else SetPowerDraw(availableModes[CurrentModeIdx].PowerDraw);
    
        if (double.IsFinite(availableModes[CurrentModeIdx].Time))
        {
            if (isPoweredOn)
            {
                if (isTimerInitialized) Timer.UnPause();
                else 
                {
                    Timer.Start((float) availableModes[CurrentModeIdx].Time);
                    isTimerInitialized = true;
                }
            }
            else
            {
                if (isTimerInitialized) Timer.Pause();
            }
        }

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

        if (!isPoweredOn) return;
        SetPowerDraw(availableModes[CurrentModeIdx].PowerDraw);

        if (double.IsFinite(availableModes[CurrentModeIdx].Time) && isTimerInitialized) Timer.Reset((float) availableModes[CurrentModeIdx].Time);
    }
}
