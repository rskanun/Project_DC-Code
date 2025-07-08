using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textField;

    [Header("���� �� ����")]
    [SerializeField] private Sprite selectSprite;
    [SerializeField] private Color selectColor;

    [Header("���� ���� �� ����")]
    [SerializeField] private Sprite deselectSprite;
    [SerializeField] private Color deselectColor;

    public void OnSelect(BaseEventData eventData)
    {
        image.sprite = selectSprite;
        textField.color = selectColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        image.sprite = deselectSprite;
        textField.color = deselectColor;
    }
}