using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSelection : MonoBehaviour
{
    [SerializeField] private Image clockImage;
    [SerializeField] private GameObject optionPivot;
    [SerializeField] private GameObject firstSelect;
    [SerializeField] private List<TextMeshProUGUI> options;

    [Title("�ð� ����")]
    [SerializeField] private Sprite normalClock;
    [SerializeField] private Sprite abyssClock;

    [Title("�ִϸ��̼� ����")]
    [SerializeField] private float angle = 17.5f;
    [SerializeField] private float selectX = 805.0f;
    [SerializeField] private float selectSize = 83.38f;
    [SerializeField] private float deselectX = 625.0f;
    [SerializeField] private float deselectSize = 71.25f;
    [SerializeField] private float normalFontAlpha = 0.77f;
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Ease ease;

    private int optionCount = 5;
    private int curIdx = 6;
    private int minIdx = 4;
    private int maxIdx = 8;

    private bool _isRolled;
    public bool IsRolled => _isRolled;

    private void OnEnable()
    {
        MapData current = GameData.Instance.CurrentMap;

        // ���ǰ� �ɿ��� ���� �ð� ��Ų �ٲٱ�
        clockImage.sprite = current.IsAbyss ? abyssClock : normalClock;
    }

    /// <summary>
    /// Ư�� index�� �ٷ� �̵�
    /// </summary>
    public void JumpTo(int index)
    {
        // ���� ���õ� �ɼ��̰ų�, �ɼ� ���� �ִϸ��̼��� ����ǰ� �ִ� ��� ����
        if (index == curIdx || _isRolled) return;

        _isRolled = true;

        // ȸ�� �ִϸ��̼�
        Vector3 endAngle = optionPivot.transform.localEulerAngles + new Vector3(0, 0, angle * (index - curIdx));
        Tween rotateAnimation = optionPivot.transform
            .DORotate(endAngle, duration, RotateMode.FastBeyond360)
            .SetEase(ease);

        // ���� �ɼ��� ���� ũ��� ��ġ�� �ǵ�����
        Sequence deselectAnimation = DOTween.Sequence()
            .Join(options[curIdx].transform.DOLocalMoveX(deselectX, duration))
            .Join(DOTween.To(() => options[curIdx].fontSize,
            x => options[curIdx].fontSize = x,
            deselectSize, duration))
            .Join(options[curIdx].DOFade(normalFontAlpha, duration))
            .SetEase(ease);

        // ��� ��ġ�� ���ο� �ɼ��� ũ��� ��ġ ����
        Sequence selectAnimation = DOTween.Sequence()
            .Join(options[index].transform.DOLocalMoveX(selectX, duration))
            .Join(DOTween.To(() => options[index].fontSize,
            x => options[index].fontSize = x,
            selectSize, duration))
            .Join(options[index].DOFade(1.0f, duration))
            .SetEase(ease);

        // �ִϸ��̼� ���ļ� ���
        DOTween.Sequence()
            .Join(rotateAnimation)
            .Join(deselectAnimation)
            .Join(selectAnimation)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                // ��� �ִϸ��̼��� ���� ��� ���� ��ġ ������Ʈ
                UpdateIndex(index);

                // ���� �̵� ��ġ ���� �غ�
                _isRolled = false;
            });
    }

    private void UpdateIndex(int index)
    {
        curIdx = index;

        // Ư�� ������ �Ѿ��ٸ� �ε巴�� �̾����� ���� ĭ ����
        if (curIdx < minIdx)
        {
            curIdx += optionCount;

            optionPivot.transform.localEulerAngles += new Vector3(0, 0, angle * optionCount);
            SelectOption(curIdx);
            DeselectOption(curIdx - optionCount);
        }
        else if (curIdx > maxIdx)
        {
            curIdx -= optionCount;

            optionPivot.transform.localEulerAngles -= new Vector3(0, 0, angle * optionCount);
            SelectOption(curIdx);
            DeselectOption(curIdx + optionCount);
        }
    }

    private void SelectOption(int index)
    {
        options[index].transform.localPosition = new Vector2(selectX, options[index].transform.localPosition.y);
        options[index].fontSize = selectSize;
        options[index].color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    private void DeselectOption(int index)
    {
        options[index].transform.localPosition = new Vector2(deselectX, options[index].transform.localPosition.y);
        options[index].fontSize = deselectSize;
        options[index].color = new Color(1.0f, 1.0f, 1.0f, normalFontAlpha);
    }
}