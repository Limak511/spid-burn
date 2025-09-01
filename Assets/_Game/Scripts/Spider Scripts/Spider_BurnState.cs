using System;
using UnityEngine;

public class Spider_BurnState : Spider_BaseState
{
    public Spider_BurnState(Spider spider) : base(spider)
    {
    }

    private float _burnMoveTimer;
    private float _burnTimer;
    private float _repathRate = .2f;
    private Vector2 _movePointPosition;

    public event Action OnBurnStarted;
    public event Action OnBurnFinished;

    public override void EnterState()
    {
        // Set faster movement
        _spider.MovementAI.SetMovementData(_spider.BurnMovementData);

        GenerateMovePoint();

        // Init burn timer
        _burnTimer = _spider.BurnTime;

        OnBurnStarted?.Invoke();
    }

    public override void ExitState()
    {
        _spider.MovementAI.ResetMovementData();
        OnBurnFinished?.Invoke();
    }

    public override void FixedUpdateState()
    {
        _spider.MovementAI.Move(_spider.Rb2d, _movePointPosition, _repathRate);
    }

    public override void UpdateState()
    {
        // When burn has finished, change to patrol
        if (_burnTimer > 0f)
        {
            _burnTimer -= Time.deltaTime;
        }
        else
        {
            _spider.StateMachine.ChangeState(_spider.PatrolState);
            return;
        }

        // When move point generated, run timer
        if (_burnMoveTimer > 0f)
        {
            _burnMoveTimer -= Time.deltaTime;
        }
        else
        {
            GenerateMovePoint();
        }
    }

    private void GenerateMovePoint()
    {
        // When move point generated, also set move timer
        _burnMoveTimer = _spider.BurnMoveTime;
        _movePointPosition = _spider.MovementAI.GetRandomSpotOnAStarGrid();
    }
}
