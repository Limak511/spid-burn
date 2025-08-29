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

    private void Update()
    {
        // Update body look at
        _renderer.flipX = !_spider.IsHeadingLeft();

        // Update moving
        _animator.SetBool(_isMovingHash, _spider.IsMoving());
    }

    private void OnEnable()
    {
        //_spider.OnBurn += Burn;
        _spider.OnDied += Die;
    }

    private void OnDisable()
    {
        //_spider.OnBurn -= Burn;
        _spider.OnDied -= Die;
    }

    private void Die()
    {
        _animator.CrossFade(_dieHash, 0f, 0);
    }

    /*private void Burn(int burnDamage)
    {
        _burnPS.Play();
    }*/
}
