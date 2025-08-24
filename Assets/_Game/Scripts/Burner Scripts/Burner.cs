using UnityEngine;

public class Burner : MonoBehaviour
{    
    private void Update()
    {
        var mousePosition = GameInput.Instance.GetPlayerWorldMousePosition();
        UpdateBurnerLookAt(mousePosition);
    }



    private void UpdateBurnerLookAt(Vector2 lookAtPosition)
    {
        var rotation = lookAtPosition - (Vector2)transform.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }

    public bool IsGunPointingLeft()
    {
        return Mathf.Abs(transform.eulerAngles.z) > 90f && Mathf.Abs(transform.eulerAngles.z) < 270f;
    }
}
