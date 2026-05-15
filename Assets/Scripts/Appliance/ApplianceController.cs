using UnityEngine;

public class ApplianceController: MonoBehaviour
{
    [SerializeField]
    private ApplianceView applianceView;

    [SerializeField]
    private Appliance appliance;


    void Start()
    {
        appliance.OnPowerToggle += (s,isOn) => applianceView.ToggleSprite(isOn);
    }

    void OnMouseDown()
    {
        appliance.TogglePower();
    }
}