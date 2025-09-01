using UnityEngine;

[RequireComponent(typeof(MovementAI))]
public class Spider : MonoBehaviour, IBurnable
{
    #region STATES

    public FiniteStateMachine StateMachine { get; private set; }
    public Spider_PatrolState PatrolState { get; private set; }
    public Spider_AttackState AttackState { get; private set; }
    public Spider_AttachToPlayerState AttachToPlayerState { get; private set; }
    public Spider_DieState DieState { get; private set; }

    private void InitStates()
    {
        PatrolState = new Spider_PatrolState(this);
        AttackState = new Spider_AttackState(this);
        AttachToPlayerState = new Spider_AttachToPlayerState(this);
        DieState = new Spider_DieState(this);
    }

    #endregion



    [field: Header("Patrol")]
    [field: SerializeField] public float PatrolPointVisionRange { get; private set; } = .38f;
    [field: SerializeField] public float IdleTimerCooldown { get; private set; } = 2f;
    [field: SerializeField] public PlayerDetector PatrolPlayerDetector { get; private set; }

    [field: Header("Attack")]
    [field: SerializeField] public float WaitBeforeAttackTimerCooldown { get; private set; } = 1f;
    [field: SerializeField] public float WaitAfterAttackTimerCooldown { get; private set; } = 1f;
    [field: SerializeField] public float AttackJumpForce { get; private set; } = 5f;
    [field: SerializeField] public PlayerDetector AttackPlayerDetector { get; private set; }

    [Header("Health")]
    [SerializeField] private int _maxHealth = 10;
    private int _health;
    public MovementAI MovementAI { get; private set; }
    public Rigidbody2D Rb2d { get; private set; }
    public PlayerController PlayerController { get; set; }



    private void Awake()
    {
        MovementAI = GetComponent<MovementAI>();
        Rb2d = GetComponent<Rigidbody2D>();
        _health = _maxHealth;
        StateMachine = new FiniteStateMachine();
        InitStates();
    }

    private void Start()
    {
        StateMachine.Start(PatrolState);
    }

    private void FixedUpdate()
    {
        StateMachine.FixedUpdate();
    }

    private void Update()
    {
        StateMachine.Update();
    }



    public bool IsHeadingLeft()
    {
        return Rb2d.linearVelocity.x < 0f;
    }
    public bool IsMoving()
    {
        float _moveThreshold = 1f;
        var isMovingX = Mathf.Abs(Rb2d.linearVelocity.x) > _moveThreshold;
        var isMovingY = Mathf.Abs(Rb2d.linearVelocity.y) > _moveThreshold;
        return isMovingX || isMovingY;
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, PatrolPointVisionRange);
    }



    // IBurnable
    public void Burn(int burnDamage)
    {
        _health -= burnDamage;
        Debug.Log(_health);

        if (_health <= 0)
        {
            StateMachine.ChangeState(DieState);
        }
    }
}
