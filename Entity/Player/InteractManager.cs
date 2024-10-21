using System.Collections.Generic;
using UnityEngine;

public class InteractManager : MonoBehaviour
{
    // 상호작용이 가능한 오브젝트 목록
    private List<InteractableObject> interactObjs = new List<InteractableObject>();

    public void RotateEyes(Vector2 direction)
    {
        if (direction == Vector2.zero)
        {
            // 멈췄을 때는 반영 안 하기
            return;
        }

        // 해당 방향으로 시야각 돌리기
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void OnInteract(PlayerManager player)
    {
        if (interactObjs.Count <= 0)
        {
            // 상호작용 할 오브젝트가 없다면 무시
            return;
        }

        // 가장 처음 접근한 오브젝트와 상호작용
        interactObjs[0].OnInteract(player);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 맞닿은 오브젝트가 상호작용 가능할 경우
        if (collision.CompareTag("Interactable Object"))
        {
            // 해당 오브젝트의 정보를 가져오기
            InteractableObject interactObj = collision.gameObject.GetComponent<InteractableObject>();
            interactObjs.Add(interactObj);

            Debug.Log($"keydown {KeyResource.Instance.Interact}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable Object"))
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
}