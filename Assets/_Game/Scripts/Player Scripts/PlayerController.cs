using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private MovementSO _movementData;
    private Rigidbody2D _rb2d;
    private Vector2 _moveInput;



    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _movementData.Move(_rb2d, _moveInput);
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
