using UnityEngine;

public class Popup : MonoBehaviour
{
    public void Close()
    {
        gameObject.SetActive(false);
    }
}