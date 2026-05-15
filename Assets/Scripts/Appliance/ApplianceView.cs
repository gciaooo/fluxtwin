using UnityEngine;

public class ApplianceView : MonoBehaviour
{
    public Sprite spriteOn;
    public Sprite spriteOff;
    private SpriteRenderer sr;
    public void ToggleSprite(bool isOn)
    {
        sr.sprite = isOn ? spriteOn : spriteOff;
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
}