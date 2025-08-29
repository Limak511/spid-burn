using UnityEngine;

public class Spider_AttachToPlayerState : Spider_BaseState
{
    public Spider_AttachToPlayerState(Spider spider) : base(spider)
    {
    }

    public override void EnterState()
    {
        Debug.Log("Attach To Player");
    }
}
