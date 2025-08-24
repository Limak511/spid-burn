using UnityEngine;

public class GameInput : MonoBehaviour
{
    public static GameInput Instance;
    private InputSystem_Actions _inputActions;



    private void Awake()
    {
        Instance = this;
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Enable();
    }

    private void OnDestroy()
    {
        _inputActions.Dispose();
    }



    public Vector2 GetPlayerMoveInput()
    {
        return _inputActions.Player.Move.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerWorldMousePosition()
    {
        var mouseScreenPosition = _inputActions.Player.MoveGun.ReadValue<Vector2>();
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        return mouseWorldPosition;
    }
}
