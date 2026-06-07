using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class ApplianceModeSwitcher : MonoBehaviour
{
    private readonly List<Toggle> modeToggles = new();
    [SerializeField]
    private TextMeshProUGUI applianceNameText;
    [SerializeField]
    private TextMeshProUGUI currentPowerDrawText;
    [SerializeField]
    private Transform togglesParent;
    [SerializeField]
    private Toggle modeTogglePrefab;
    [SerializeField]
    private Button statusButton;
    [SerializeField]
    private Sprite statusOn;
    [SerializeField]
    private Sprite statusOff;

    private void SetToggles(Appliance appliance)
    {
        for (int i = 0; i < appliance.AvailableModes.Count; i++)
        {
            ApplianceMode mode = appliance.AvailableModes[i];
            Toggle toggle = Instantiate(modeTogglePrefab, transform.position, transform.rotation, togglesParent);
            toggle.name = mode.Name + "ModeToggle";
            toggle.transform.Find("Label").GetComponent<TextMeshProUGUI>().text = mode.Name;
            toggle.group = togglesParent.gameObject.GetComponent<ToggleGroup>();
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
        applianceNameText.text = name;

        UpdatePowerDraw(appliance.CurrentPowerDraw);

        SetToggles(appliance);
        modeToggles[appliance.CurrentModeIdx].isOn = true;

        statusButton.onClick.AddListener(appliance.TogglePower);
    }
    public void UpdatePowerDraw(double powerDraw)
    {
        currentPowerDrawText.text = powerDraw.ToString() + "W";
    }

    public void ToggleStatusButtonSprite(bool isOn)
    {
        statusButton.GetComponent<Image>().sprite = isOn ? statusOn : statusOff;
    }

    public void DeactivateModeSwitcher()
    {
        gameObject.SetActive(false);
    }

    void Start()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    
}