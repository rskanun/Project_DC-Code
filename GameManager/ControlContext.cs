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
    private Dictionary<Type, IController> controllers = new Dictionary<Type, IController>();
    private HashSet<IController> activeControllers = new HashSet<IController>();

    public ControlContext()
    {
        KeyInput.Player.Enable();
        KeyInput.UI.Enable();
    }

    public void RegisterController(IController controller)
    {
        // 컨트롤러 등록
        controllers.Add(controller.GetType(), controller);
    }

    public void RemoveController(IController controller)
    {
        // 컨트롤러 삭제
        controllers.Remove(controller.GetType());
    }

    public void SetState(IController controller)
    {
        // 기존 컨트롤러 연결 끊기
        CurrentState?.OnDisconnected();

        // 새 컨트롤러 연결
        CurrentState = controller;
        CurrentState?.OnConnected();

        // 해당 컨트롤러가 등록되지 않은 컨트롤러인 경우
        if (!controllers.ContainsValue(controller))
        {
            // 메모리에 추가
            RegisterController(controller);
        }
    }

    public void SetState(Type type)
    {
        // 등록된 컨트롤러가 아니라면 무시
        if (!controllers.ContainsKey(type)) return;

        // 기존 컨트롤러 연결 끊기
        CurrentState?.OnDisconnected();

        // 새 컨트롤러 연결
        CurrentState = controllers[type];
        CurrentState?.OnConnected();
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