using System;
using UnityEngine;
using Zenject;

public class Star : PlayerTrigger, IPause
{
    public Action OnCollect;
    private bool _isPause;
    private PauseService _pauseService;
    
    [Inject]
    public void Constructor(PauseService pauseService)
    {
        _pauseService = pauseService;
    }
    
    private void Awake()
    {
        PlayerEnter += Collect;
    }

    private void OnEnable()
    {
        _pauseService.AddPause(this);
    }
    
    private void OnDisable()
    {
        _pauseService.RemovePause(this);
    }

    private void Update()
    {
        if(!_isPause)
            transform.Rotate(new Vector3(1,1,0));
    }

    private void Collect(PlayerMovement player)
    {
        player.GetComponent<Animator>().SetTrigger("GetStar");
        OnCollect?.Invoke();
        Destroy(gameObject);
    }

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }
}
