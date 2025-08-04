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

    // �ִϸ��̼� ����
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

        // ���̵��� ���� �ð� ���� ����
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
        // �̹� �ִϸ��̼� ���� ���̰ų� ������ �ʿ䰡 ���ٸ� ����
        if (isRolled) yield break;

        isRolled = true;

        // ���� ������ ����Ͽ� ȸ������ ���� �� ���
        int count = Enum.GetValues(typeof(Difficulty)).Length;
        int nextIndex = ((int)current + (rotateClockwise ? 1 : -1) + count) % count;
        Difficulty nextDifficulty = (Difficulty)nextIndex;

        // �ش� ���� ���簪���� ����
        current = nextDifficulty;
        onChanged?.Invoke(current);

        // ȸ�� ���� ���� ����
        float moveAngle = rotateClockwise ? 120 : -120;
        Vector3 endAngle = difficultyClock.localEulerAngles + new Vector3(0, 0, moveAngle);

        // �ִϸ��̼� ����
        yield return difficultyClock
            .DOLocalRotate(endAngle, duration, RotateMode.FastBeyond360)
            .SetEase(ease)
            .SetUpdate(true)
            .WaitForCompletion();

        // �ִϸ��̼��� ���� ��, ���� ������Ʈ
        UpdateDescription();

        isRolled = false;
    }
}