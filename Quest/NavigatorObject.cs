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
            // ���� ���� ���
            Vector3 direction = (targetObject.transform.position - transform.position).normalized;

            // ȸ�� ���� ���
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Ÿ�� �������� ȭ��ǥ ������
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}