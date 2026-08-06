using System.Collections.Generic;
using UnityEngine;

public class CircuitLine: MonoBehaviour, IElecLink
{
    public double MaximumPowerDraw;
    public ApartmentSource ApartmentSourceParent;
    public List<WallPlug> wallPlugs = new();
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
        Debug.Log("Shutdown CircuitLine");
        foreach (WallPlug w in wallPlugs)
        {
            w.Shutdown();
        }
    }

    public void OnChildPowerDrawChange()
    {
        if(TotalPowerDraw() > MaximumPowerDraw) {
            Debug.Log("Power exceeded!");
        }
        ApartmentSourceParent.OnChildPowerDrawChange();
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