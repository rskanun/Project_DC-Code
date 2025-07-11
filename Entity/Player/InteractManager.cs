using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    // 상호작용이 가능한 오브젝트 목록
    private HashSet<InteractableObject> interactObjs = new HashSet<InteractableObject>();

    // 상호작용 액션 코루틴
    private Coroutine curInteractAction;
    private InteractableObject curInteractObj;

    public void ClearInteractObjs()
    {
        interactObjs.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 맞닿은 오브젝트가 상호작용 가능할 경우
        if (IsInteractableObj(collision))
        {
            // 해당 오브젝트의 정보를 가져오기
            InteractableObject interactObj = collision.gameObject.GetComponent<InteractableObject>();
            interactObjs.Add(interactObj);

            //Debug.Log($"keydown {KeyResource.Instance.Interact}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (IsInteractableObj(collision))
        {
            InteractableObject obj = collision.GetComponent<InteractableObject>();

            // 범위에서 벗어난 오브젝트가 현재 상호작용 가능한 오브젝트일 경우
            if (interactObjs.Contains(obj))
            {
                // 오브젝트의 정보를 초기화
                interactObjs.Remove(obj);
                Debug.Log($"{collision.name} exit");
            }
        }
    }

    private bool IsInteractableObj(Collider2D collision)
    {
        return collision.CompareTag("NPC")
            || collision.CompareTag("Portal")
            || collision.CompareTag("Gimmik Object");
    }

    /************************************************************
     * [상호작용]
     * 
     * 오브젝트에 따른 상호작용
     ************************************************************/

    public void OnInteract(PlayerManager player)
    {
        if (interactObjs.Count <= 0 || curInteractAction != null)
        {
            // 상호작용 할 오브젝트가 없거나 이미 상호작용 중일 경우 무시
            return;
        }

        // 가장 처음 접근한 오브젝트와 상호작용
        curInteractObj = interactObjs.First();
        curInteractAction = StartCoroutine(InteractAction(player));
    }

    public void OnInteractCancel()
    {
        if (curInteractObj.IsInteractCanceled) // 캔슬할 수 있는 상호작용만 캔슬
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
        // 기믹 오브젝트일 경우에만 기믹 수행
        if (curInteractObj is GimmikObject obj)
        {
            InitAction();

            // 특정 조건을 만족할 때까지 상호작용 액션을 취함
            while (IsCompletedAction() == false)
            {
                yield return new WaitUntil(() => OnAction());
            }
        }

        OnCompletedAction(player);
    }

    private void InitAction()
    {
        // 상호작용 액션 준비
    }

    private bool OnAction()
    {
        // 기믹 수행 행동 대기
        return true;
    }

    private bool IsCompletedAction()
    {
        // 기믹 수행이 끝났는 지 확인
        return false;
    }

    private void OnCompletedAction(PlayerManager player)
    {
        // 상호작용 액션 끝내기
        curInteractAction = null;

        // 오브젝트와의 상호작용 수행
        curInteractObj.OnInteractive(player);
        curInteractObj = null;
    }
}