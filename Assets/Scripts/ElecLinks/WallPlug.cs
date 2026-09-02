using System;
using System.Collections.Generic;
using UnityEngine;

public class WallPlug : MonoBehaviour, IElecLink
{
    public double MaximumPowerDraw; 
    public CircuitLine CircuitLineParent;

    public List<Appliance> appliances = new();

    public event EventHandler<double> OnPowerSurge;
    public event EventHandler<double> OnTotalPowerDrawChange;

    public void AddAppliance(Appliance app)
    {
        if (appliances.Find(x => x == app) == null) appliances.Add(app);
    }

    public void RemoveAppliance(Appliance app)
    {
        appliances.Remove(app);
    }

    public double TotalPowerDraw()
    {
        double t = 0;
        foreach (Appliance appliance in appliances)
        {
            t += appliance.CurrentPowerDraw;
        }
        return t;
    }
    
    public void Shutdown()
    {
        OnPowerSurge?.Invoke(this, TotalPowerDraw());
        foreach(Appliance appliance in appliances)
        {
            if (appliance.CurrentPowerDraw != 0)
            {
                appliance.TogglePower();
            }
        }
    }

    public void OnChildPowerDrawChange()
    {
        double t = TotalPowerDraw();
        OnTotalPowerDrawChange?.Invoke(this, t);
        if(t > MaximumPowerDraw)
        {
            Shutdown();
        }
        CircuitLineParent.OnChildPowerDrawChange();
    }

    void Start()
    {
        CircuitLineParent.AddWallPlug(this);
    }

    public GameObject GetParent()
    {
        return CircuitLineParent.gameObject;
    }
}