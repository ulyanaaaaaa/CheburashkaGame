using System;
using UnityEngine;
using Zenject;

public class LevelStateMachine : MonoBehaviour
{
    public Action<LevelState> OnStateChange;
    
    private LevelState _currentLevelState = LevelState.Game;

    private FinishTrigger _finishTrigger;
    private FailTrigger _failTrigger;
    
    [Inject]
    public void Constructor(FinishTrigger finishTrigger, FailTrigger failTrigger)
    {
        _finishTrigger = finishTrigger;
        _failTrigger = failTrigger;
    }

    private void OnEnable()
    {
        _finishTrigger.OnFinish += Finish;
        _failTrigger.OnFail += Fail;
    }

    private void OnDisable()
    {
        _finishTrigger.OnFinish -= Finish;
        _failTrigger.OnFail -= Fail;
    }

    public void ChangeState(LevelState state)
    {
        if(_currentLevelState == state)
            return;
        
        OnStateChange?.Invoke(state);
        _currentLevelState = state;
    }

    private void Finish()
    {
        ChangeState(LevelState.Finish);
    }  
    
    private void Fail()
    {
        Debug.Log("Change state to fail");
        ChangeState(LevelState.Fail);
    }
}

public enum LevelState
{
    Pause,
    Resume, 
    Game,
    Finish,
    Fail
}
