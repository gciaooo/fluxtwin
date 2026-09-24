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
    private TextMeshProUGUI timeLeftText;
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

    private ApplianceTimer applianceTimer;
    private void SetToggles(Appliance appliance)
    {
        for (int i = 0; i < appliance.AvailableModes.Count; i++)
        {
            ApplianceMode mode = appliance.AvailableModes[i];
            Toggle toggle = Instantiate(modeTogglePrefab, transform.position, transform.rotation, togglesParent);
            toggle.name = mode.Name + "ModeToggle";
            
            TextMeshProUGUI toggleText = toggle.transform.Find("Label").GetComponent<TextMeshProUGUI>(); 
            toggleText.text = mode.Name;
            if (!double.IsInfinity(mode.Time)) {
                String modeDuration = TimeSpan.FromSeconds(mode.Time).ToString("ss"); 
                toggleText.text += $" ({modeDuration}s)";
            }
            toggle.group = togglesParent.gameObject.GetComponent<ToggleGroup>();
            
            int idx = i;
            toggle.onValueChanged.AddListener(isToggleOn => { 
                if (isToggleOn) appliance.SwitchMode(idx);
            });
            
            modeToggles.Add(toggle);
        }
    }

    private void SetTimerText()
    {
        if (applianceTimer.IsActive == false)
            return;
        timeLeftText.text = TimeSpan.FromSeconds(applianceTimer.TimeRemaining).ToString(@"mm\:ss");
    }

    public void SetSwitcherData(String name, Appliance appliance)
    {
        gameObject.name = name + "ModeSwitcher";
        applianceNameText.text = name;

        UpdatePowerDraw(appliance.CurrentPowerDraw);

        SetToggles(appliance);
        modeToggles[appliance.CurrentModeIdx].isOn = true;

        statusButton.onClick.AddListener(appliance.TogglePower);

        applianceTimer = appliance.Timer;
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
        timeLeftText.text = TimeSpan.FromSeconds(0).ToString(@"mm\:ss");
    }

    void Update()
    {
        SetTimerText();
    }

}