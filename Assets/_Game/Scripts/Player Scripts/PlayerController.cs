using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    private Rigidbody2D _rb2d;
    private Vector2 _moveInput;



    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb2d.AddForce(_moveInput * _moveSpeed);
    }

    private void Update()
    {
        _moveInput = GameInput.Instance.GetPlayerMoveInput();
    }



    public bool IsMoving()
    {
        return _moveInput != Vector2.zero;
    }
}
