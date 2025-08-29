using UnityEngine;

public interface IState
{
    void EnterState();
    void ExitState();
    void UpdateState();
    void FixedUpdateState();
    void OnCollisionEnter2DState(Collision2D collision);
    void OnTriggerEnter2DState(Collider2D collider);
}
