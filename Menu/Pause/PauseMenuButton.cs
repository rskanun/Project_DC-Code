using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuButton : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textField;

    [Header("선택 시 설정")]
    [SerializeField] private Sprite selectSprite;
    [SerializeField] private Color selectColor;

    [Header("선택 해제 시 설정")]
    [SerializeField] private Sprite deselectSprite;
    [SerializeField] private Color deselectColor;

    private void OnEnable()
    {
        GameObject selectObj = EventSystem.current.currentSelectedGameObject;

        if (selectObj == gameObject) SetSelectOption();
        else SetDeselectOption();
    }

    public void OnSelect(BaseEventData eventData)
    {
        SetSelectOption();
    }

    private void SetSelectOption()
    {
        image.sprite = selectSprite;
        textField.color = selectColor;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        SetDeselectOption();
    }

    public void SetDeselectOption()
    {
        image.sprite = deselectSprite;
        textField.color = deselectColor;
    }
}