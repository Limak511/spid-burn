using UnityEngine;

public class FiniteStateMachine
{
    public IState CurrentState { get; private set; }

    public void Start(IState startState)
    {
        ChangeState(startState);
    }

    public void Update()
    {
        if (CurrentState != null)
        {
            CurrentState.UpdateState();
        }
    }

    public void ChangeState(IState state)
    {
        if (CurrentState != null)
        {
            CurrentState.ExitState();
        }

        CurrentState = state;
        CurrentState.EnterState();
    }

    public void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.FixedUpdateState();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (CurrentState != null)
        {
            CurrentState.OnCollisionEnter2DState(collision);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (CurrentState != null)
        {
            CurrentState.OnTriggerEnter2DState(collider);
        }
    }

}
