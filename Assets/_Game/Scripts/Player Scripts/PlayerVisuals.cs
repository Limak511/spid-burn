using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    // hashed animations
    private int _isMovingHash = Animator.StringToHash(StringReferences.PLAYER___IS_MOVING);



    private PlayerController _playerController;
    private Animator _animator;
    private SpriteRenderer _renderer;
    private Burner _burner;



    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _burner = GetComponentInChildren<Burner>();
    }

    private void Update()
    {
        // update move animation
        _animator.SetBool(_isMovingHash, _playerController.IsMoving());

        // update body looking direction
        _renderer.flipX = _burner.IsGunPointingLeft();
    }
}
