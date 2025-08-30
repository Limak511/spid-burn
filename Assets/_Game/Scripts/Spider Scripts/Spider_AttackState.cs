using System.Collections;
using UnityEngine;

public class Spider_AttackState : Spider_BaseState
{
    private Coroutine _attackCoroutine;

    public Spider_AttackState(Spider spider) : base(spider)
    {
    }

    public override void EnterState()
    {
        _attackCoroutine = _spider.StartCoroutine(CO_Attack());
        _spider.AttackPlayerDetector.Enable();
    }

    public override void ExitState()
    {
        _spider.AttackPlayerDetector.OnPlayerDetected -= AttachToPlayer;
        _spider.AttackPlayerDetector.Disable();
        _spider.StopCoroutine(_attackCoroutine);

    }

    private void AttachToPlayer(PlayerController playerController)
    {
        _spider.StateMachine.ChangeState(_spider.AttachToPlayerState);
        _spider.StopCoroutine(_attackCoroutine);
    }

    private IEnumerator CO_Attack()
    {
        // Wait before attack
        yield return new WaitForSeconds(_spider.WaitBeforeAttackTimerCooldown);

        // Activate player detection
        _spider.AttackPlayerDetector.OnPlayerDetected += AttachToPlayer;

        // Attack player
        var directionToPlayer = (_spider.PlayerController.transform.position - _spider.transform.position).normalized;
        _spider.Rb2d.AddForce(directionToPlayer * _spider.AttackJumpForce, ForceMode2D.Impulse);

        // Wait after attack and switch to patrol
        yield return new WaitForSeconds(_spider.WaitAfterAttackTimerCooldown);
        _spider.StateMachine.ChangeState(_spider.PatrolState);
    }
}
