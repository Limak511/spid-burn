using UnityEngine;

[RequireComponent(typeof(MovementAI))]
public class Spider : MonoBehaviour
{
    [SerializeField] private float _patrolSpotVisionRange = .5f;
    [SerializeField] private float _idleTimerCooldown = 1.5f;
    private MovementAI _movementAI;
    private Rigidbody2D _rb2d;
    private Vector2 _patrolSpot;
    private float _repathRate = .2f;
    private float _idleTimer;

    private void Awake()
    {
        _movementAI = GetComponent<MovementAI>();
        _rb2d = GetComponent<Rigidbody2D>();
        _patrolSpot = _movementAI.GetRandomSpotOnAStarGrid();
    }

    private void FixedUpdate()
    {
        // Timer for idling
        if (_idleTimer > 0)
        {
            _idleTimer -= Time.deltaTime;
            return;
        }

        _movementAI.Move(_rb2d, _patrolSpot, _repathRate);
    }

    private void Update()
    {
        // If close to patrol spot, get new patrol spot position
        if (Vector2.Distance(transform.position, _patrolSpot) < _patrolSpotVisionRange)
        {
            _idleTimer = _idleTimerCooldown;
            _patrolSpot = _movementAI.GetRandomSpotOnAStarGrid();
        }
    }



    public bool IsHeadingLeft()
    {
        return _rb2d.linearVelocity.x < 0f;
    }

    public bool IsMoving()
    {
        float _moveThreshold = 1f;
        var isMovingX = Mathf.Abs(_rb2d.linearVelocity.x) > _moveThreshold;
        var isMovingY = Mathf.Abs(_rb2d.linearVelocity.y) > _moveThreshold;
        return isMovingX || isMovingY;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _patrolSpotVisionRange);
    }
}
