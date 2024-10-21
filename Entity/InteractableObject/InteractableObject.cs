using System.Collections;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public bool IsCanceled; // 상호작용 모션 도중 취소할 수 있는 지
    private Coroutine curInteractAction;

    public void OnInteract(PlayerManager player)
    {
        if (curInteractAction != null)
        {
            // 이미 상호작용 액션을 하고 있을 경우 무시
            return;
        }

        curInteractAction = StartCoroutine(InteractAction(player));
    }

    public void OnCancel()
    {
        if (IsCanceled) // 캔슬할 수 있는 상호작용만 캔슬
        {
            if (curInteractAction == null)
            {
                // 취소할 상호작용이 없는 경우 무시
                return;
            }

            // 현재 진행 중인 상호작용 액션 취소
            StopCoroutine(curInteractAction);
            curInteractAction = null;
        }
    }

    private IEnumerator InteractAction(PlayerManager player)
    {
        InitAction();

        // 특정 조건을 만족할 때까지 상호작용 액션을 취함
        while (IsCompletedAction() == false)
        {
            yield return new WaitUntil(() => OnAction());
        }

        curInteractAction = null;
        OnCompletedAction(player);
    }

    public virtual void InitAction()
    {
        // 상호작용 액션 준비
    }

    public abstract bool OnAction();

    public abstract bool IsCompletedAction();

    public abstract void OnCompletedAction(PlayerManager player);
}