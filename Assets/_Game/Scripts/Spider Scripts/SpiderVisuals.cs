using UnityEngine;

public class SpiderVisuals : MonoBehaviour
{
    private int _isMovingHash = Animator.StringToHash(StringReferences.SPIDER___IS_MOVING);
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
}
