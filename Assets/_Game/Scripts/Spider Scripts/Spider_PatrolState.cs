using UnityEngine;

public class Spider_PatrolState : Spider_BaseState
{
    private Vector2 _patrolPointPosition;
    private float _repathRate = .2f;
    private float _idleTimer = 0f;

    public Spider_PatrolState(Spider spider) : base(spider)
    {
    }

    public override void EnterState()
    {
        _idleTimer = 0f;
        _patrolPointPosition = GetNewPatrolPointPosition();
        _spider.PatrolPlayerDetector.Enable();
        _spider.PatrolPlayerDetector.OnPlayerDetected += OnPlayerDetected;
    }

    public override void ExitState()
    {
        _spider.PatrolPlayerDetector.OnPlayerDetected -= OnPlayerDetected;
        _spider.PatrolPlayerDetector.Disable();
    }

    private void OnPlayerDetected(PlayerController playerController)
    {
        if (playerController.GetComponent<PlayerSpiderAttacher>().CanAttachSpider())
        {
            _spider.PlayerController = playerController;
            _spider.StateMachine.ChangeState(_spider.AttackState);
        }
    }

    public override void FixedUpdateState()
    {
        if (_idleTimer > 0f)
        {
            return;
        }

        _spider.MovementAI.Move(_spider.Rb2d, _patrolPointPosition, _repathRate);
    }

    public override void UpdateState()
    {
        if (_idleTimer > 0f)
        {
            _idleTimer -= Time.deltaTime;
        }

        // when close to patrol point, switch to idle state
        if (Vector2.Distance(_spider.transform.position, _patrolPointPosition) < _spider.PatrolSpotVisionRange)
        {
            _patrolPointPosition = GetNewPatrolPointPosition();
            _idleTimer = _spider.IdleTimerCooldown;
        }
    }

    private Vector2 GetNewPatrolPointPosition()
    {
        return _spider.MovementAI.GetRandomSpotOnAStarGrid();
    }
}
