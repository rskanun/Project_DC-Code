using UnityEngine;
using System;
using System.Collections.Generic;

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

    private Controller _currentState;
    public Controller CurrentState
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
    private Dictionary<Type, Controller> controllers = new();
    private HashSet<Controller> connectControllers = new HashSet<Controller>();

    public ControlContext()
    {
        KeyInput.Player.Enable();
        KeyInput.UI.Enable();
    }

    public void RegisterController(Controller controller)
    {
        controllers.Add(controller.GetType(), controller);
    }

    public void RemoveController(Controller controller)
    {
        if (controller == null) return;

        controllers.Remove(controller.GetType());
    }

    public void ConnectController(Type type)
    {
        if (!controllers.ContainsKey(type))
        {
            Debug.LogWarning($"{type} is not a registered controller!");
            return;
        }

        Controller connectController = controllers[type];

        connectController.OnConnected();
        connectControllers.Add(connectController);
    }

    public void DisconnectController(Type type)
    {
        if (!controllers.ContainsKey(type))
        {
            Debug.LogWarning($"{type} is not a registered controller!");
            return;
        }

        Controller disconnectController = controllers[type];

        disconnectController.OnDisconnected();
        connectControllers.Remove(disconnectController);
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