using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    private void OnEnable()
    {
        ControlContext.Instance.RegisterController(this);
    }

    private void OnDisable()
    {
        ControlContext.Instance.RemoveController(this);
    }

    public abstract void OnConnected();
    public abstract void OnDisconnected();
}