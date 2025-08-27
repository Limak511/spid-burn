using UnityEngine;

[CreateAssetMenu(fileName = "New_Movement", menuName = "Game Data/Movement")]
public class MovementSO : ScriptableObject
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _decceleration;

    public void Move(Rigidbody2D rb2d, Vector2 moveDir)
    {
        //Calculate the direction we want to move in and our desired velocity
        var targetSpeed = moveDir * _maxSpeed;

        //We can reduce are control using Lerp() this smooths changes to are direction and speed
        //targetSpeed = Mathf.Lerp(rb2d.linearVelocity.x, targetSpeed, lerpAmount);
        
        // Calculate acceleration rate
        float accelRateX = (Mathf.Abs(targetSpeed.x) > 0.01f) ? _acceleration : _decceleration;
        float accelRateY = (Mathf.Abs(targetSpeed.y) > 0.01f) ? _acceleration : _decceleration;

        //Calculate difference between current velocity and desired velocity
        var speedDif = targetSpeed - rb2d.linearVelocity;

        //Calculate force along x-axis to apply to thr player
        var movement = speedDif * new Vector2(accelRateX, accelRateY);

        //Convert this to a vector and apply to rigidbody
        rb2d.AddForce(movement, ForceMode2D.Force);
    }
}
