using System;

public class FailTrigger : PlayerTrigger
{
    public Action OnFail;

    private void Start()
    {
        PlayerEnter += Fail;
    }

    private void Fail(PlayerMovement playerMovement)
    {
        OnFail?.Invoke();
    }
}
