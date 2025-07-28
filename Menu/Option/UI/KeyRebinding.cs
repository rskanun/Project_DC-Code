using System.Collections.Generic;
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
    private bool is2DVector; <- �̰� �� ����ϰ�

    [SerializeField]
    [OnValueChanged(nameof(OnActionChanged))]
    [ValueDropdown(nameof(GetActions))]
    private string actionName;

    [SerializeField]
    [OnValueChanged(nameof(OnActionChanged))]
    [ValueDropdown(nameof(GetSchemes))]
    private string schemeName;

    [SerializeField]
    [ShowIf(nameof(is2DVector), true)]
    [ValueDropdown(nameof(GetBindings))]
    private string bindingName;

    private InputActionRebindingExtensions.RebindingOperation rebindOperation;
    private InputAction action;

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateDisplayName();
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

        if (action == null || !is2DVector) return list;

        // �ش� action�� ���ε��� �͵� ����
        for (int i = 0; i < action.bindings.Count; i++)
        {
            var binding = action.bindings[i];
            if (binding.isPartOfComposite && binding.groups.Contains(schemeName))
            {
                string label = $"{binding.name} ({action.GetBindingDisplayString(i).Replace("/", "��")})";
                list.Add(label, binding.name);
            }
        }

        return list;
    }

    private void OnActionChanged()
    {
        // Action ���� �ٲ�� binding ���� ����
        bindingName = string.Empty;
    }
#endif

    private void OnEnable()
    {
        UpdateDisplayName();
    }

    public void KeyRebind()
    {
        // �����ε��� Ű�� ������ ����
        if (action == null) return;

        var bindings = action.bindings.ToList();
        int bindingIndex = is2DVector ? bindings.FindIndex(b => b.isPartOfComposite && b.name == bindingName)
                            : bindings.FindIndex(b => !b.isPartOfComposite && !b.isComposite);

        // �����ε��� Ű�� ã�� �� ������ ����
        if (bindingIndex == -1) return;

        // Ű �����ε� ����
        StartRebind(bindingIndex);
    }

    private void StartRebind(int bindingIndex)
    {
        // ���� ���ε� ����
        action.Disable();

        // ������ �������� �����ε� ����
        rebindOperation?.Cancel();

        string test = action.bindings[bindingIndex].effectivePath;
        // Ű �����ε�
        Debug.Log("����");
        rebindOperation = action.PerformInteractiveRebinding(bindingIndex)
            .WithControlsExcluding("Mouse") // ���콺 Ŭ���� ����
            .OnMatchWaitForAnother(0.1f)
            .OnComplete(operation =>
            {
                operation.Dispose();
                action.Enable(); // ���� ���ε� ��Ȱ��ȭ

                Debug.Log($"��! '{test}' -> '{action.bindings[bindingIndex].effectivePath}'");

                // UI �ݿ�
                UpdateDisplayName();
            })
            .Start();
    }


    private void UpdateDisplayName()
    {
        // �׼ǰ� �Ҵ�
        action = FindRuntimeAction(actionName);

        if (action == null)
        {
            textField.text = "";
            return;
        }

        var bindings = action.bindings.ToList();
        int index = !is2DVector ? bindings.FindIndex(b => !b.isComposite && !b.isPartOfComposite)
                        : bindings.FindIndex(b => b.isPartOfComposite && b.name == bindingName);

        // ���� ã�� ���ߴٸ� �� �� �Ҵ�
        if (index == -1)
        {
            textField.text = "";
            return;
        }

        // �� �Ҵ�
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