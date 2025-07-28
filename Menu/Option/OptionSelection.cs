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

    [Title("시계 설정")]
    [SerializeField] private Sprite normalClock;
    [SerializeField] private Sprite abyssClock;

    [Title("애니메이션 설정")]
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

        // 현실과 심연에 따른 시계 스킨 바꾸기
        clockImage.sprite = current.IsAbyss ? abyssClock : normalClock;
    }

    /// <summary>
    /// 특정 index로 바로 이동
    /// </summary>
    public void JumpTo(int index)
    {
        // 현재 선택된 옵션이거나, 옵션 선택 애니메이션이 실행되고 있는 경우 무시
        if (index == curIdx || _isRolled) return;

        _isRolled = true;

        // 회전 애니메이션
        Vector3 endAngle = optionPivot.transform.localEulerAngles + new Vector3(0, 0, angle * (index - curIdx));
        Tween rotateAnimation = optionPivot.transform
            .DORotate(endAngle, duration, RotateMode.FastBeyond360)
            .SetEase(ease);

        // 이전 옵션을 원래 크기와 위치로 되돌리기
        Sequence deselectAnimation = DOTween.Sequence()
            .Join(options[curIdx].transform.DOLocalMoveX(deselectX, duration))
            .Join(DOTween.To(() => options[curIdx].fontSize,
            x => options[curIdx].fontSize = x,
            deselectSize, duration))
            .Join(options[curIdx].DOFade(normalFontAlpha, duration))
            .SetEase(ease);

        // 가운데 위치할 새로운 옵션의 크기와 위치 변경
        Sequence selectAnimation = DOTween.Sequence()
            .Join(options[index].transform.DOLocalMoveX(selectX, duration))
            .Join(DOTween.To(() => options[index].fontSize,
            x => options[index].fontSize = x,
            selectSize, duration))
            .Join(options[index].DOFade(1.0f, duration))
            .SetEase(ease);

        // 애니메이션 합쳐서 재생
        DOTween.Sequence()
            .Join(rotateAnimation)
            .Join(deselectAnimation)
            .Join(selectAnimation)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                // 모든 애니메이션이 끝난 경우 현재 위치 업데이트
                UpdateIndex(index);

                // 다음 이동 위치 받을 준비
                _isRolled = false;
            });
    }

    private void UpdateIndex(int index)
    {
        curIdx = index;

        // 특정 구간을 넘었다면 부드럽게 이어지기 위해 칸 당기기
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