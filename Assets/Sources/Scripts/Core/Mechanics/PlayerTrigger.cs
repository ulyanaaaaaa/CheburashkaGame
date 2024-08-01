using System;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    public Action<PlayerMovement> PlayerEnter;
    public Action PlayerExit;

    public void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement playerMovement))
            PlayerEnter?.Invoke(playerMovement);
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out PlayerMovement playerMovement))
            PlayerExit?.Invoke();
    }
}
