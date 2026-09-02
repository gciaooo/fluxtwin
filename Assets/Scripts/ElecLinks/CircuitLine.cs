using System;
using System.Collections.Generic;
using UnityEngine;

public class CircuitLine: MonoBehaviour, IElecLink
{
    public double MaximumPowerDraw;
    public ApartmentSource ApartmentSourceParent;
    public List<WallPlug> wallPlugs = new();
    public event EventHandler<double> OnPowerSurge;
    public event EventHandler<double> OnTotalPowerDrawChange;
    public void AddWallPlug(WallPlug plug)
    {
        if (wallPlugs.Find(x => x == plug) == null) wallPlugs.Add(plug);
    }

    public void RemoveWallPlug(WallPlug plug)
    {
        wallPlugs.Remove(plug);
    }
    public double TotalPowerDraw()
    {
        double t = 0;
        foreach (WallPlug w in wallPlugs)
        {
            t += w.TotalPowerDraw();
        }
        return t;
    }

    public void Shutdown()
    {
        OnPowerSurge?.Invoke(this, TotalPowerDraw());
        foreach (WallPlug w in wallPlugs)
        {
            w.Shutdown();
        }
    }

    public void OnChildPowerDrawChange()
    {
        double t = TotalPowerDraw();
        OnTotalPowerDrawChange?.Invoke(this, t);
        if(t > MaximumPowerDraw) {
            Shutdown();
        }
        ApartmentSourceParent.OnChildPowerDrawChange();
    }
    public void DebugLogStats(double total)
    {
        String message = $"CircuitLine {gameObject.name}\nPower:{total}W";
        Debug.Log(message);
    }
    void Start()
    {
        ApartmentSourceParent.AddCircuitLine(this);
    }

    public GameObject GetParent()
    {
        return ApartmentSourceParent.gameObject;
    }
}