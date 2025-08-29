using System;
using UnityEngine;

[RequireComponent(typeof(MovementAI))]
public class Spider : MonoBehaviour, IBurnable
{
    [Header("Patrol")]
    [SerializeField] private float _patrolSpotVisionRange = .5f;
    [SerializeField] private float _idleTimerCooldown = 1.5f;
    private Vector2 _patrolSpot;
    private float _idleTimer;

    [Header("Health")]
    [SerializeField] private int _maxHealth = 10;
    private int _health;

    [Header("Burn")]
    [SerializeField] private float _burnTimerCooldown;
    private float _burnTimer;
    [SerializeField] private MovementSO _burnMovementData;

    private MovementAI _movementAI;
    private float _repathRate = .2f;
    private Rigidbody2D _rb2d;
    private bool _hasDied = false;

    //public event Action<int> OnBurn;
    public event Action OnDied;



    private void Awake()
    {
        _movementAI = GetComponent<MovementAI>();
        _rb2d = GetComponent<Rigidbody2D>();
        _patrolSpot = _movementAI.GetRandomSpotOnAStarGrid();
        _health = _maxHealth;
    }

    private void FixedUpdate()
    {
        // If idling wait (don't move)
        if (_idleTimer > 0f)
        {
            //_idleTimer -= Time.deltaTime;
            return;
        }

        _movementAI.Move(_rb2d, _patrolSpot, _repathRate);
    }

    private void Update()
    {
        // If dead stop updating spider
        if (_hasDied)
        {
            return;
        }

        // Timer for idling
        if (_idleTimer > 0f)
        {
            _idleTimer -= Time.deltaTime;
        }

        // Timer for burning
        if (_burnTimer > 0f)
        {
            _burnTimer -= Time.deltaTime;
        }

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

    // IBurnable
    public void Burn(int burnDamage)
    {
        _health -= burnDamage;
        Debug.Log(_health);

        // Set movement to faster
        _movementAI.SetMovementData(_burnMovementData);

        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Spider is dead");
        _hasDied = true;
        OnDied?.Invoke();
    }
}
