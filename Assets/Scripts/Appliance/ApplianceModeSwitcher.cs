using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ToggleGroup))]
public class ApplianceModeSwitcher : MonoBehaviour
{
    private List<Toggle> modeToggles = new();

    [SerializeField]
    private Toggle modeTogglePrefab;

    private void SetToggles(Appliance appliance)
    {
        for (int i = 0; i < appliance.AvailableModes.Count; i++)
        {
            ApplianceMode mode = appliance.AvailableModes[i];
            Toggle toggle = Instantiate(modeTogglePrefab, transform);
            toggle.name = mode.Name + "ModeToggle";
            toggle.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = mode.Name;
            toggle.group = GetComponent<ToggleGroup>();
            int idx = i;
            toggle.onValueChanged.AddListener(isToggleOn => { 
                if (isToggleOn) appliance.SwitchMode(idx);
            });
            
            modeToggles.Add(toggle);
        }
    }

    public void SetSwitcherData(String name, Appliance appliance)
    {
        gameObject.name = name + "ModeSwitcher";
        SetToggles(appliance);
        if (appliance.CurrentModeIdx != -1) modeToggles[appliance.CurrentModeIdx].isOn = true;
    }
}