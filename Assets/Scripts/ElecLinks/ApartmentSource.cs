using System.Collections.Generic;
using UnityEngine;

public class ApartmentSource: MonoBehaviour, IElecLink
{
    [SerializeField]
    public double MaximumPowerDraw;
    public List<CircuitLine> circuitLines = new(); 
    public void AddCircuitLine(CircuitLine line)
    {
        if (circuitLines.Find(x => x == line) == null) circuitLines.Add(line);
    }

    public void RemoveAppliance(CircuitLine line)
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
        if(TotalPowerDraw() > MaximumPowerDraw) {
            Debug.Log("Power exceeded!");
        }
    }
    public void Shutdown()
    {
        Debug.Log("Shutdown ApartmentSource");
        foreach (CircuitLine c in circuitLines)
        {
            c.Shutdown();
        }
    }

    void Start()
    {
        Shutdown();
    }
}