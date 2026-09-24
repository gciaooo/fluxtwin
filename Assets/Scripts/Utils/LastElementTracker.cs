using System;
using UnityEngine;

public class LastElementTracker: MonoBehaviour
{
    public enum ElemType
    {
        ApartmentSource = 0,
        CircuitLine = 1,
        WallPlug = 2,
        Appliance = 3
    }
    private GameObject[] lastElecLinks = new GameObject[3];
    private int[] elemCounts = {0,0,0,0};
    
    public void OnElementInstantiated(GameObject elem)
    {
        ElemType? type = FetchElemType(elem);
        if (type == null)
        {
            Debug.LogError("Unknown element instantiated " + elem);
            return;
        }

        LinkElem(elem, type.GetValueOrDefault());
        if (type != ElemType.Appliance) lastElecLinks[(int)type] = elem;
    }

    private ElemType? FetchElemType(GameObject elem)
    {
        if (elem.GetComponent<ApplianceController>() != null)
            return ElemType.Appliance;
        if (elem.GetComponent<WallPlug>() != null)
            return ElemType.WallPlug;
        if (elem.GetComponent<CircuitLine>() != null)
            return ElemType.CircuitLine;
        if (elem.GetComponent<ApartmentSource>() != null)
            return ElemType.ApartmentSource;
        return null;
    }

    public String nameElem(GameObject elem)
    {
        ElemType? type = FetchElemType(elem);
        String result = (type.ToString() ?? "Unknown") + elemCounts[(int)type];
        elemCounts[(int)type]++;
        return result; 
    }

    private void LinkElem(GameObject elem, ElemType type)
    {
        if (type == ElemType.ApartmentSource) return;
        switch(type)
        {
            case ElemType.CircuitLine:
                elem.GetComponent<CircuitLine>().ApartmentSourceParent = lastElecLinks[(int)type-1].GetComponent<ApartmentSource>();
                return;
            case ElemType.WallPlug:
                elem.GetComponent<WallPlug>().CircuitLineParent = lastElecLinks[(int)type-1].GetComponent<CircuitLine>();
                return;
            case ElemType.Appliance:
                elem.GetComponent<ApplianceController>().parentWallPlug = lastElecLinks[(int)type-1].GetComponent<WallPlug>();
                return;
        }
    }
}