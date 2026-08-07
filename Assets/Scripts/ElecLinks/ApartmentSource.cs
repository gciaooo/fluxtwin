using System;
using System.Collections.Generic;
using UnityEngine;

public class ApartmentSource: MonoBehaviour, IElecLink
{
    [SerializeField]
    public double MaximumPowerDraw;
    public List<CircuitLine> circuitLines = new(); 
    public event EventHandler<double> OnPowerSurge;
    public void AddCircuitLine(CircuitLine line)
    {
        if (circuitLines.Find(x => x == line) == null) circuitLines.Add(line);
    }

    public void RemoveCircuitLine(CircuitLine line)
    {
        circuitLines.Remove(line);
    }
    public double TotalPowerDraw()
    {
        double t = 0;
        foreach (CircuitLine c in circuitLines)
        {
            t += c.TotalPowerDraw();
        }
        return t;
    }

    public void OnChildPowerDrawChange()
    {
        double t = TotalPowerDraw();
        if(t > MaximumPowerDraw) {
            Shutdown();
        }
    }
    public GameObject GetParent()
    {
        Debug.LogError("Called GetParent on top-level object");
        return null;
    }

    public void Shutdown()
    {

        OnPowerSurge?.Invoke(this, TotalPowerDraw());
        foreach (CircuitLine c in circuitLines)
        {
            c.Shutdown();
        }
    }
}