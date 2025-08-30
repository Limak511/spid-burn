using UnityEngine;

public class Spider_AttachToPlayerState : Spider_BaseState
{
    private PlayerSpiderAttacher _playerSpiderAttacher;

    public Spider_AttachToPlayerState(Spider spider) : base(spider)
    {
    }

    public override void EnterState()
    {
        _playerSpiderAttacher = _spider.PlayerController.GetComponent<PlayerSpiderAttacher>();

        if (_playerSpiderAttacher.CanAttachSpider())
        {
            _playerSpiderAttacher.AttachSpider();
            Object.Destroy(_spider.gameObject);
        }
        else
        {
            _spider.StateMachine.ChangeState(_spider.PatrolState);
        }
    }
}
