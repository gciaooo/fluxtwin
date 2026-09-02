
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ApartmentSource))]
public class ApartmentSourceView: MonoBehaviour
{
   private ApartmentSource apartmentSource;
    [SerializeField]
    private TextMeshPro powerText;

    void Start()
    {
        apartmentSource = GetComponent<ApartmentSource>();
        apartmentSource.OnPowerSurge += (s, total) => DebugLogStats(total);

        apartmentSource.OnTotalPowerDrawChange += (s, total)
        => SetPowerDrawText(total);


        SetPowerDrawText(0);
    }

    private void SetPowerDrawText(double total)
    {
        powerText.text = $"{total} / {apartmentSource.MaximumPowerDraw}";         
    }

    public void DebugLogStats(double total)
    {
        String message = $"ApartmentSource {gameObject.name}\nPower: {total}W, Limit :{apartmentSource.MaximumPowerDraw}";
        if (total <= apartmentSource.MaximumPowerDraw) Debug.Log(message);
        else Debug.LogWarning(message);
    }
};