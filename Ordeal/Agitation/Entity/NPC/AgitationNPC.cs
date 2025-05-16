using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgitationNPC : AgitationEntity, ISelectHandler, IDeselectHandler
{
    static public AgitationNPC SelectedNPC { get; private set; }
    static private AgitationNPC firstSelect;
    public bool isFirstSelect;

    [SerializeField] private GameObject selectMark;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // ���ο� NPC�� ù �������� Ȱ��ȭ �� ���
        if (isFirstSelect && firstSelect != this)
        {
            // ���� ���� �ִٸ�, �ش� NPC�� ��Ȱ��ȭ
            if (firstSelect != null)
            {
                firstSelect.isFirstSelect = false;
            }

            // �ش� NPC�� ù ���� NPC�� ����
            firstSelect = this;
        }
    }
#endif

    private void Start()
    {
        // ���� ù ���� NPC�� ��� �ش� NPC�� ���� ���·� ����
        if (isFirstSelect)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnClick()
    {
        // �ش� ĳ���͸� Ŭ������ ��� ���� ������Ʈ�� ����
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        SelectedNPC = this;

        // �ش� NPC�� ���õǾ��� ��, ���� ��ũ Ȱ��ȭ
        selectMark.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // �ش� NPC�� ������ �����Ǿ��� ��, ���� ��ũ ��Ȱ��ȭ
        selectMark.SetActive(false);
    }

    /************************************************************
    * [��ǥ]
    * 
    * NPC���� ���� ��Ȳ�� �ڽ��� ����(AI)�� ���� ��ǥ ����
    ************************************************************/

    public virtual AgitationEntity GetVotedTarget()
    {
        List<AgitationEntity> entities = AgitationGameData.Instance.Entities;

        // �� ��ƼƼ���� ���� ������ ������ ���� ���� �̱�
        int sum = entities.Sum(e => e.Stat.AgitationLevel);
        int random = Random.Range(0, sum);

        int cumulative = 0;
        foreach (AgitationEntity entity in entities)
        {
            cumulative += entity.Stat.AgitationLevel;
            if (random < cumulative)
                return entity;
        }

        return null;
    }
}