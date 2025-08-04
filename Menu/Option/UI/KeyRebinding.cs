using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyRebinding : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private TextMeshProUGUI textField;

    [SerializeField]
    [OnValueChanged(nameof(OnActionChanged))]
    [ValueDropdown(nameof(GetActions))]
    private string actionName;

    [SerializeField]
    [OnValueChanged(nameof(OnActionChanged))]
    [ValueDropdown(nameof(GetSchemes))]
    private string schemeName;

    [SerializeField]
    [ShowIf(nameof(hasBinding), true)]
    [ValueDropdown(nameof(GetBindings))]
    private string bindingName;

    private bool hasBinding;

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private InputAction action;

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateDisplayName();
        UpdateHasBinding();
    }

    private ValueDropdownList<string> GetActions()
    {
        var list = new ValueDropdownList<string>();

        if (inputActions == null)
            return list;

        foreach (var map in inputActions.actionMaps)
        {
            foreach (var action in map.actions)
            {
                string fullName = $"{map.name}/{action.name}";
                list.Add(fullName, fullName);
            }
        }

        return list;
    }

    private ValueDropdownList<string> GetSchemes()
    {
        var list = new ValueDropdownList<string>();

        if (action == null) return list;

        foreach (var scheme in inputActions.controlSchemes)
        {
            list.Add(scheme.name, scheme.name);
        }

        return list;
    }

    private ValueDropdownList<string> GetBindings()
    {
        var list = new ValueDropdownList<string>();

        if (action == null || !hasBinding) return list;

        // 해당 action에 바인딩된 것들 수집
        for (int i = 0; i < action.bindings.Count; i++)
        {
            var binding = action.bindings[i];
            if (binding.isPartOfComposite && binding.groups.Contains(schemeName))
            {
                string label = $"{binding.name} ({action.GetBindingDisplayString(i).Replace("/", "／")})";
                list.Add(label, binding.name);
            }
        }

        return list;
    }

    private void OnActionChanged()
    {
        // Action 값이 바뀌면 binding 값도 리셋
        bindingName = string.Empty;

        // binding 필요 여부 검사
        UpdateHasBinding();
    }

    private void UpdateHasBinding()
    {
        // binding 필요 여부 업데이트
        hasBinding = FindRuntimeAction(actionName).bindings.Any(b => b.isPartOfComposite);
    }
#endif

    private void OnEnable()
    {
        UpdateDisplayName();
    }

    public void KeyRebind()
    {
        // 리바인딩할 키가 없으면 무시
        if (action == null) return;

        var bindings = action.bindings.ToList();
        int bindingIndex = hasBinding ? bindings.FindIndex(b => b.isPartOfComposite && b.name == bindingName)
                            : bindings.FindIndex(b => !b.isPartOfComposite && !b.isComposite);

        // 리바인딩할 키를 찾을 수 없으면 무시
        if (bindingIndex == -1) return;

        // 키 리바인딩 시작
        StartRebind(bindingIndex);
    }

    private void StartRebind(int bindingIndex)
    {
        // 현재 바인딩 해제
        action.Disable();

        // 기존에 실행중인 리바인딩 해제
        rebindOperation?.Cancel();

        string test = action.bindings[bindingIndex].effectivePath;
        // 키 리바인딩
        Debug.Log("시작");
        rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse") // 마우스 클릭은 제외
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                operation.Dispose();
                action.Enable(); // 현재 바인딩 재활성화

                Debug.Log($"끝! '{test}' -> '{action.bindings[bindingIndex].effectivePath}'");

                // UI 반영
                UpdateDisplayName();
            })
            .Start();
    }


    private void UpdateDisplayName()
    {
        // 액션값 할당
        action = FindRuntimeAction(actionName);

        // 액션값을 못찾은 경우 빈 값 할당
        if (action == null)
        {
            textField.text = "";
            return;
        }

        var bindings = action.bindings.ToList();
        int index = -1;

        if (!hasBinding)
        {
            // 단일 바인딩 중 현재 scheme에 속한 것만 선택
            index = bindings.FindIndex(b =>
                !b.isComposite &&
                !b.isPartOfComposite &&
                b.groups != null &&
                b.groups.Contains(schemeName)
            );
        }
        else
        {
            // composite 바인딩 중에서 선택된 bindingName과 일치하고 scheme에 속하는 것만 선택
            index = bindings.FindIndex(b =>
                b.isPartOfComposite &&
                b.name == bindingName &&
                b.groups != null &&
                b.groups.Contains(schemeName)
            );
        }

        // 값을 찾지 못했다면 빈 값 할당
        if (index == -1)
        {
            textField.text = "";
            return;
        }

        // 값 할당
        textField.text = action.GetBindingDisplayString(index);
    }

    private InputAction FindRuntimeAction(string fullName)
    {
        var split = fullName.Split('/');
        if (split.Length != 2) return null;

        string mapName = split[0];
        string actionName = split[1];

        var actionMap = ControlContext.Instance.KeyInput.asset.FindActionMap(mapName);
        return actionMap?.FindAction(actionName);
    }
}