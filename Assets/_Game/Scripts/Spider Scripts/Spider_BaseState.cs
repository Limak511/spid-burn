using UnityEngine;

public abstract class Spider_BaseState : IState
{
    protected readonly Spider _spider;
    protected Spider_BaseState(Spider spider)
    {
        _spider = spider;
    }

    public virtual void EnterState() { }
    public virtual void ExitState() { }
    public virtual void FixedUpdateState() { }
    public virtual void OnCollisionEnter2DState(Collision2D collision) { }
    public virtual void OnTriggerEnter2DState(Collider2D collider) { }
    public virtual void UpdateState() { }
}
