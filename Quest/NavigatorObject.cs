using UnityEngine;

public class NavigatorObject : MonoBehaviour
{
    [SerializeField]
    private GameObject naviArrow;
    private GameObject targetObject;

    public void SetTarget(GameObject targetObject)
    {
        if (targetObject == null)
        {
            // �� ������Ʈ�� ��� ���� ����
            return;
        }

        naviArrow.SetActive(true);
        this.targetObject = targetObject;
    }

    public void OnComplete()
    {
        // ��ǥ�� �����ϰ� �Ǹ� Ÿ���� �����ϰ� ������ ����
        targetObject = null;
        naviArrow.SetActive(false);
    }

    private void Update()
    {
        if (targetObject != null)
        {
            // ���� ���� ���
            Vector3 direction = (targetObject.transform.position - transform.position).normalized;

            // ȸ�� ���� ���
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;

            // Ÿ�� �������� ȭ��ǥ ������
            gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}