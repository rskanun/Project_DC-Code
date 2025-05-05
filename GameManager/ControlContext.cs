using UnityEngine;
using System;
using System.Collections.Generic;



#if UNITY_EDITOR
using UnityEditor;
#endif

public class ControlContext
{
    private static ControlContext _instance;
    public static ControlContext Instance
    {
        get
        {
            if (_instance == null)
                _instance = new ControlContext();

            return _instance;
        }
    }

    private IController _currentState;
    public IController CurrentState
    {
        private set { _currentState = value; }
        get { return _currentState; }
    }

    private MainInput _keyInput;
    public MainInput KeyInput
    {
        get
        {
            if (_keyInput == null)
                _keyInput = new MainInput();

            return _keyInput;
        }
    }

    private bool _keyBlock;
    public bool KeyBlock
    {
        private set { _keyBlock = value; }
        get { return _keyBlock; }
    }

    // ���� ��ϵ� ��Ʈ�ѷ� ���
    private Dictionary<Type, IController> controllers = new Dictionary<Type, IController>();
    private HashSet<IController> activeControllers = new HashSet<IController>();

    public ControlContext()
    {
        KeyInput.Player.Enable();
        KeyInput.UI.Enable();
    }

    public void RegisterController(IController controller)
    {
        // ��Ʈ�ѷ� ���
        controllers.Add(controller.GetType(), controller);
    }

    public void RemoveController(IController controller)
    {
        // ��Ʈ�ѷ� ����
        controllers.Remove(controller.GetType());
    }

    public void SetState(IController controller)
    {
        // ���� ��Ʈ�ѷ� ���� ����
        CurrentState?.OnDisconnected();

        // �� ��Ʈ�ѷ� ����
        CurrentState = controller;
        CurrentState?.OnConnected();

        // �ش� ��Ʈ�ѷ��� ��ϵ��� ���� ��Ʈ�ѷ��� ���
        if (!controllers.ContainsValue(controller))
        {
            // �޸𸮿� �߰�
            RegisterController(controller);
        }
    }

    public void SetState(Type type)
    {
        // ��ϵ� ��Ʈ�ѷ��� �ƴ϶�� ����
        if (!controllers.ContainsKey(type)) return;

        // ���� ��Ʈ�ѷ� ���� ����
        CurrentState?.OnDisconnected();

        // �� ��Ʈ�ѷ� ����
        CurrentState = controllers[type];
        CurrentState?.OnConnected();
    }

    public void KeyLock()
    {
        KeyBlock = true;

        // ��� Ű �� ��Ȱ��ȭ
        KeyInput.Player.Disable();
        KeyInput.UI.Disable();
    }

    public void KeyUnlock()
    {
        KeyBlock = false;

        // ��� Ű �� �ٽ� Ȱ��ȭ
        KeyInput.Player.Enable();
        KeyInput.UI.Enable();
    }
}