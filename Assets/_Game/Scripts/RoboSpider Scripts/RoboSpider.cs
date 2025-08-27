using UnityEngine;

[RequireComponent(typeof(MovementAI))]
public class RoboSpider : MonoBehaviour
{
    [SerializeField] private float _patrolSpotVisionRange;
    private MovementAI _movementAI;
    private Rigidbody2D _rb2d;
    private Vector2 _patrolSpot;
    float _repathRate = .2f;



    private void Awake()
    {
        _movementAI = GetComponent<MovementAI>();
        _rb2d = GetComponent<Rigidbody2D>();
        _patrolSpot = _movementAI.GetRandomSpotOnAStarGrid();
    }

    private void FixedUpdate()
    {
        _movementAI.Move(_rb2d, _patrolSpot, _repathRate);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, _patrolSpot) < _patrolSpotVisionRange)
        {
            _patrolSpot = _movementAI.GetRandomSpotOnAStarGrid();
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, _patrolSpotVisionRange);
    }
}
