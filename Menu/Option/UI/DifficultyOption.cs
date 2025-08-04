using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;

public enum Difficulty
{
    Easy = 0,
    Normal = 1,
    Hard = 2
}

public class DifficultyOption : MonoBehaviour, IMoveHandler
{
    [SerializeField] private Transform difficultyClock;
    [SerializeField] private LocalizeStringEvent descriptionEvent;
    [SerializeField] private UnityEvent<Difficulty> onChanged;

    private Dictionary<Difficulty, LocalizedString> descriptionDict;
    private Difficulty current;

    // 애니메이션 설정
    private float duration = 0.3f;
    private Ease ease = Ease.InExpo;
    private bool isRolled;

    private void Awake()
    {
        descriptionDict = new()
        {
            {Difficulty.Easy, new LocalizedString("Option_Table", "Difficulty_Easy_Description")},
            {Difficulty.Normal, new LocalizedString("Option_Table", "Difficulty_Normal_Description")},
            {Difficulty.Hard, new LocalizedString("Option_Table", "Difficulty_Hard_Description")},
        };
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        UpdateDescription();
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        current = difficulty;

        // 난이도에 따른 시계 각도 설정
        float angle = (int)difficulty * 120.0f;
        difficultyClock.localRotation = Quaternion.Euler(0, 0, angle);
    }

    public void UpdateDescription()
    {
        descriptionEvent.StringReference = descriptionDict[current];
    }

    public void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Left) RollLeft();
        else if (eventData.moveDir == MoveDirection.Right) RollRight();
    }

    public void RollLeft()
    {
        StartCoroutine(RollClock(false));
    }

    public void RollRight()
    {
        StartCoroutine(RollClock(true));
    }

    private IEnumerator RollClock(bool rotateClockwise)
    {
        // 이미 애니메이션 실행 중이거나 실행할 필요가 없다면 무시
        if (isRolled) yield break;

        isRolled = true;

        // 도는 방향을 계산하여 회전했을 때의 값 계산
        int count = Enum.GetValues(typeof(Difficulty)).Length;
        int nextIndex = ((int)current + (rotateClockwise ? 1 : -1) + count) % count;
        Difficulty nextDifficulty = (Difficulty)nextIndex;

        // 해당 값을 현재값으로 설정
        current = nextDifficulty;
        onChanged?.Invoke(current);

        // 회전 종료 각도 설정
        float moveAngle = rotateClockwise ? 120 : -120;
        Vector3 endAngle = difficultyClock.localEulerAngles + new Vector3(0, 0, moveAngle);

        // 애니메이션 실행
        yield return difficultyClock
            .DOLocalRotate(endAngle, duration, RotateMode.FastBeyond360)
            .SetEase(ease)
            .SetUpdate(true)
            .WaitForCompletion();

        // 애니메이션이 끝난 후, 설명 업데이트
        UpdateDescription();

        isRolled = false;
    }
}