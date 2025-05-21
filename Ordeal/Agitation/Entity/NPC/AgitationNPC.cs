using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class AgitationNPC : AgitationEntity, ISelectHandler, IDeselectHandler
{
    static public AgitationNPC SelectedNPC { get; private set; }
    static private AgitationNPC firstSelect;
    public bool isFirstSelect;

    [SerializeField]
    private AgitationTrait trait;

    [SerializeField] private GameObject selectMark;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // 새로운 NPC가 첫 선택으로 활성화 된 경우
        if (isFirstSelect && firstSelect != this)
        {
            // 이전 값이 있다면, 해당 NPC는 비활성화
            if (firstSelect != null)
            {
                firstSelect.isFirstSelect = false;
            }

            // 해당 NPC를 첫 선택 NPC로 설정
            firstSelect = this;
        }
    }
#endif

    private void Start()
    {
        // 만약 첫 선택 NPC인 경우 해당 NPC를 선택 상태로 변경
        if (isFirstSelect)
            EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnClick()
    {
        // 해당 캐릭터를 클릭했을 경우 선택 오브젝트로 설정
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    public void OnSelect(BaseEventData eventData)
    {
        SelectedNPC = this;

        // 해당 NPC가 선택되었을 시, 선택 마크 활성화
        selectMark.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // 해당 NPC의 선택이 해제되었을 시, 선택 마크 비활성화
        selectMark.SetActive(false);
    }

    /************************************************************
    * [투표]
    * 
    * NPC들은 현재 상황과 자신의 성격(AI)에 따라 투표 진행
    ************************************************************/

    public AgitationEntity GetVotedTarget()
    {
        return trait.GetVotedTarget(this, AgitationGameData.Instance.Entities);
    }
}