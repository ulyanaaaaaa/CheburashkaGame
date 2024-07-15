using System;

public class FinishTrigger : PlayerTrigger
{
    public Action OnFinish;

    private void Awake()
    {
        PlayerEnter += Finish;
    }

    private void Finish()
    {
        OnFinish?.Invoke();
    }
}
