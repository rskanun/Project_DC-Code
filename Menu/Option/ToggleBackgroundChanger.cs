using UnityEngine;
using UnityEngine.UI;

public class ToggleBackgroundChanger : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image background;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;

    public void OnToggleChanged()
    {
        Sprite sprite = toggle.isOn ? onSprite : offSprite;

        background.sprite = sprite;
    }
}