using System.Collections.Generic;
using UnityEngine;

public class WallPlug : MonoBehaviour, IElecLink
{
    public double MaximumPowerDraw; 
    public CircuitLine CircuitLineParent;

    public List<Appliance> appliances = new();

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
        Debug.Log("Shutdown WallPlug");
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
        if(TotalPowerDraw() > MaximumPowerDraw)
        {
            Shutdown();
        }
        CircuitLineParent.OnChildPowerDrawChange();
    }

    void Start()
    {
        CircuitLineParent.AddWallPlug(this);
    }

    public Vector3 GetParentConnectionPoint()
    {
        return CircuitLineParent.transform.position;
    }
}