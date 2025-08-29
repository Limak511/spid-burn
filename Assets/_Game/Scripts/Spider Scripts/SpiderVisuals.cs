using UnityEngine;

public class SpiderVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem _burnPS;

    private int _isMovingHash = Animator.StringToHash(StringReferences.SPIDER___IS_MOVING);
    private int _dieHash = Animator.StringToHash(StringReferences.SPIDER___DIE);
    private Animator _animator;
    private Spider _spider;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spider = GetComponent<Spider>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _spider.DieState.OnDied += Die;
    }

    private void OnDestroy()
    {
        _spider.DieState.OnDied -= Die;
    }

    private void Update()
    {
        // Update body look at if spider is moving by x axis
        if (Mathf.Abs(_spider.Rb2d.linearVelocity.x) > Mathf.Epsilon)
        {
            _renderer.flipX = !_spider.IsHeadingLeft();
        }

        // Update moving
        _animator.SetBool(_isMovingHash, _spider.IsMoving());
    }

    private void Die()
    {
        _animator.CrossFade(_dieHash, 0f, 0);
    }
}
