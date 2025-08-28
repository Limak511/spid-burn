using UnityEngine;

public class BurnerVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fireParticles;
    private SpriteRenderer _renderer;
    private Burner _burner;
    private ParticleSystem.ShapeModule _shapeModule;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _burner = GetComponent<Burner>();
    }

    private void Start()
    {
        // Set fire particles to match spread angle
        _shapeModule = _fireParticles.shape;
        _shapeModule.arc = _burner.FireSpreadAngle;
    }

    private void Update()
    {
        // handle flipping gun when pointing in certain directions
        _renderer.flipY = _burner.IsPointingLeft();

        // Control fire particles
        if (GameInput.Instance.IsPlayerAttackPressed())
        {
            _fireParticles.Play();
        }
        else
        {
            _fireParticles.Stop();
        }
    }
}
