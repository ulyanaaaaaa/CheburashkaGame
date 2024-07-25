using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public Action OnEnter;
    public Action OnExit;
    
    public void OnTriggerEnter(Collider other)
    {
        OnEnter?.Invoke();
    }

    public void OnTriggerExit(Collider other)
    {
        OnExit?.Invoke();
    }
}
