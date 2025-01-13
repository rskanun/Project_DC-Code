using UnityEngine;

public class NavigatorObject : MonoBehaviour
{
    private GameObject targetObject;

    public void SetTarget(GameObject targetObject)
    {
        this.targetObject = targetObject;
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