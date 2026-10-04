using UnityEngine;
using UnityEngine.UI;
//Enables Dropdown behaviour and animations for bringing the SpawnMenu up and down.
public class SpawnMenuResizer: MonoBehaviour
{
    private Animator spawnMenuAnimator;
    //Parameter specific of the SpawnMenuAnimController animation controller.
    private readonly int animationParameterId = Animator.StringToHash("IsOpen");
    private Toggle expander;
    private Image expanderImage;
    
    [SerializeField] private Sprite isOnSprite;
    [SerializeField] private Sprite isOffSprite;
    
    private void Awake()
    {
        spawnMenuAnimator = GetComponent<Animator>();
        expander = GetComponentInChildren<Toggle>();
        expanderImage = expander.gameObject.GetComponent<Image>();
        expander.onValueChanged.AddListener(OnExpanderValueChanged);
    }
    private void OnExpanderValueChanged(bool isOn)
    {
        spawnMenuAnimator.SetBool(animationParameterId, isOn);
        expanderImage.sprite = isOn ? isOnSprite : isOffSprite;
    }


}