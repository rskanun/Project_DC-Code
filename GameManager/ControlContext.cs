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
    private HashSet<IController> connectControllers = new HashSet<IController>();

    public ControlContext()
    {
        KeyInput.Player.Enable();
        KeyInput.UI.Enable();
    }

    public void ConnectController(IController controller)
    {
        controller.OnConnected();
        connectControllers.Add(controller);
    }

    public void DisconnectController(IController controller)
    {
        // ���� ����Ǿ� ���� ���� ��Ʈ�ѷ��� ��� ����
        if (!connectControllers.Contains(controller)) return;

        controller.OnDisconnected();
        connectControllers.Remove(controller);
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