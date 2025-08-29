using System;

public class Spider_DieState : Spider_BaseState
{
    public event Action OnDied;

    public Spider_DieState(Spider spider) : base(spider)
    {
    }

    public override void EnterState()
    {
        OnDied?.Invoke();
    }
}
