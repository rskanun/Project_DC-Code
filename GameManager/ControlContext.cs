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

    // 현재 등록된 컨트롤러 목록
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
        // 현재 연결되어 있지 않은 컨트롤러인 경우 무시
        if (!connectControllers.Contains(controller)) return;

        controller.OnDisconnected();
        connectControllers.Remove(controller);
    }

    public void KeyLock()
    {
        KeyBlock = true;

        // 모든 키 맵 비활성화
        KeyInput.Player.Disable();
        KeyInput.UI.Disable();
    }

    public void KeyUnlock()
    {
        KeyBlock = false;

        // 모든 키 맵 다시 활성화
        KeyInput.Player.Enable();
        KeyInput.UI.Enable();
    }
}