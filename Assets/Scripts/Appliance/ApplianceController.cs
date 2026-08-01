using UnityEngine;
using UnityEngine.EventSystems;

public class ApplianceController: MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private ApplianceView applianceView;

    [SerializeField]
    private Appliance appliance;

    [SerializeField]
    private WallPlug parentWallPlug;

    void Start()
    {
        parentWallPlug.AddAppliance(appliance);
        appliance.OnPowerToggle += (s,isOn) => applianceView.ToggleStatusSprites(isOn);
        appliance.OnPowerDrawChange += (s,powerDraw) => 
        {
            applianceView.UpdatePowerDrawView(powerDraw);
            parentWallPlug.OnChildPowerDrawChange();
        };
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            applianceView.SpawnModeSwitcher(appliance);   
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        applianceView.ChangeAlpha(0.5f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        applianceView.ChangeAlpha(0f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        Vector3 newPos = Camera.main.ScreenToWorldPoint(eventData.position);
        newPos.z = 0;
        transform.position = newPos;
    }
}