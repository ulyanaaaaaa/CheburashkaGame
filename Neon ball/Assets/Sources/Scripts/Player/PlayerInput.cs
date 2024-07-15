using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action OnForward;
    public Action OnLeft;
    public Action OnRight;
    public Action OnBack;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
            OnForward?.Invoke();
        
        if (Input.GetKey(KeyCode.S))
            OnBack?.Invoke();
        
        if (Input.GetKey(KeyCode.D))
            OnRight?.Invoke();
        
        if (Input.GetKey(KeyCode.A))
            OnLeft?.Invoke();
    }
}