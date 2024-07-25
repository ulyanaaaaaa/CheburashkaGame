using System;
using System.Collections;
using UnityEngine;

public class FinishTrigger : PlayerTrigger
{
    public Action OnFinish;
    private float _reloadTime = 2f;

    private void Awake()
    {
        PlayerEnter += Finish;
    }

    private void Finish(PlayerMovement playerMovement)
    {
        playerMovement.GetComponent<Animator>().SetTrigger("Finish");
        StartCoroutine(ReloadSceneTick());
    }

    private IEnumerator ReloadSceneTick()
    {
        yield return new WaitForSeconds(_reloadTime);
        OnFinish?.Invoke();
    }
}
