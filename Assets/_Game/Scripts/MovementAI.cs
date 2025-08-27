using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class MovementAI : MonoBehaviour
{
    [SerializeField] private MovementSO _movementData;
    [SerializeField] private float _nextWaypointDistance = .5f;
    private bool _reachedEndOfPath;
    private int _currentWaypoint = 0;
    private Path path;
    private Seeker _seeker;
    private float _lastRepath = float.NegativeInfinity;

    public void SetMovementData(MovementSO movementData)
    {
        _movementData = movementData;
    }



    private void Awake()
    {
        _seeker = GetComponent<Seeker>();
        _seeker.pathCallback += OnPathComplete;
    }

    private void OnDestroy()
    {
        _seeker.pathCallback -= OnPathComplete;
    }

    private void OnPathComplete(Path p)
    {
        p.Claim(this);
        if (!p.error)
        {
            if (path != null)
            {
                path.Release(this);
            }
            path = p;
            _currentWaypoint = 0;
        }
        else
        {
            p.Release(this);
        }
    }

    public void Move(Rigidbody2D rb, Vector2 targetPosition, float repathRate = .4f)
    {
        if (Time.time > _lastRepath + repathRate && _seeker.IsDone())
        {
            _lastRepath = Time.time;
            _seeker.StartPath(transform.position, targetPosition);
        }

        // check if path is null
        if (path == null) return;

        _reachedEndOfPath = false;
        float distanceToWaypoint;

        while (true)
        {
            distanceToWaypoint = Vector2.Distance(transform.position, path.vectorPath[_currentWaypoint]);
            if (distanceToWaypoint < _nextWaypointDistance)
            {
                if (_currentWaypoint + 1 < path.vectorPath.Count)
                {
                    _currentWaypoint++;
                }
                else
                {
                    _reachedEndOfPath = true;
                    break;
                }
            }
            else
            {
                break;
            }
        }

        Vector2 direction = ((Vector2)path.vectorPath[_currentWaypoint] - (Vector2)transform.position).normalized;
        _movementData.Move(rb, direction);
    }

    public Vector2 GetRandomSpotOnAStarGrid()
    {
        var gridGraph = AstarPath.active.data.gridGraph;
        var gridNodes = new List<GraphNode>();
        gridGraph.GetNodes(node =>
        {
            if (node.Walkable)
            {
                gridNodes.Add(node);
            }
        });
        var randomNode = gridNodes[Random.Range(0, gridNodes.Count)];
        return (Vector3)randomNode.position;
    }
}
