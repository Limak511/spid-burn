using UnityEngine;

public class BurnerVisuals : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Burner _burner;



    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _burner = GetComponent<Burner>();
    }

    private void Update()
    {
        // handle flipping gun when pointing in certain directions
        _renderer.flipY = _burner.IsGunPointingLeft();
    }
}
