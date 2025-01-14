using UnityEngine;

public class NavigatorObject : MonoBehaviour
{
    private GameObject targetObject;

    public void SetTarget(GameObject targetObject)
    {
        if (targetObject == null)
        {
            // 빈 오브젝트의 경우 받지 않음
            return;
        }

        this.targetObject = targetObject;
    }

    public void OnComplete()
    {
        // 목표에 도달하게 되면 타겟을 리셋하고서 스스로 꺼짐
        targetObject = null;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (targetObject != null)
        {
            // 방향 벡터 계산
            Vector3 direction = (targetObject.transform.position - transform.position).normalized;

            // 회전 각도 계산
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 타겟 방향으로 화살표 돌리기
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}