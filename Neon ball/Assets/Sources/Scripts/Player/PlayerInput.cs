using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Action OnForward;
    public Action OnLeft;
    public Action OnRight;
    public Action OnBack;
    public Action OnRun;
    public Action OnIdle;
    public Action OnJump;

    private bool _isPause;

    private void Awake()
    {
        _isPause = GetComponent<PlayerMovement>().IsPause;
    }

    private void Update()
    {
        if (_isPause)
            return;
        
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift))
            OnRun?.Invoke();
        else if (Input.GetKey(KeyCode.W))
            OnForward?.Invoke();
        else
            OnIdle?.Invoke();
        
        if (Input.GetKey(KeyCode.S))
            OnBack?.Invoke();
        
        if (Input.GetKey(KeyCode.D))
            OnRight?.Invoke();
        
        if (Input.GetKey(KeyCode.A))
            OnLeft?.Invoke();
        
        if (Input.GetKey(KeyCode.Space))
            OnJump?.Invoke();
    }
}
