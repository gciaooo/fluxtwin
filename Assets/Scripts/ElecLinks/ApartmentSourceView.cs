
using System;
using UnityEngine;

[RequireComponent(typeof(ApartmentSource))]
public class ApartmentSourceView: MonoBehaviour
{
   private ApartmentSource apartmentSource;

    void Start()
    {
        apartmentSource = GetComponent<ApartmentSource>();
        apartmentSource.OnPowerSurge += (s, total) => DebugLogStats(total);
    }
    public void DebugLogStats(double total)
    {
        String message = $"ApartmentSource {gameObject.name}\nPower: {total}W, Limit :{apartmentSource.MaximumPowerDraw}";
        if (total <= apartmentSource.MaximumPowerDraw) Debug.Log(message);
        else Debug.LogWarning(message);
    }
};