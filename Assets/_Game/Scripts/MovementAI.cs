using Pathfinding;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Seeker))]
public class MovementAI : MonoBehaviour
{
    //[Header("Local Avoidance")]
    //[SerializeField] private float _maxSeeAhead = .5f;
    //[SerializeField] private LayerMask _avoidLayer;
    //[SerializeField] private float _avoidancePushAwayForce = 2f;
    [SerializeField] private MovementSO _movementData;
    [SerializeField] private float _nextWaypointDistance = .5f;
    private bool _reachedEndOfPath;
    private int _currentWaypoint = 0;
    private Path path;
    private Seeker _seeker;
    private float _lastRepath = float.NegativeInfinity;
    private Vector2 _direction;

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



    /// <summary>
    /// Move GameObject to target position. USE ONLY IN FIXED UPDATE
    /// </summary>
    /// <param name="rb">Rigidbody2D of GameObject we want to move</param>
    /// <param name="targetPosition">Positon where we want to move GameObject</param>
    /// <param name="repathRate">Path's update rate</param>
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

        _direction = ((Vector2)path.vectorPath[_currentWaypoint] - (Vector2)transform.position).normalized;

        // Local avoidance
        //var hits = Physics2D.RaycastAll(transform.position, _direction, _maxSeeAhead, _avoidLayer);
        //foreach (var hit in hits)
        //{
        //    if (hit.transform == transform)
        //    {
        //        continue;
        //    }

        //    _direction = (_direction - (Vector2)hit.transform.position).normalized * _avoidancePushAwayForce;
        //}

        _movementData.Move(rb, _direction);
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



    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            // Local avoidance - see ahead vector
            //Gizmos.color = Color.yellow;
            //Gizmos.DrawRay(transform.position, _direction * _maxSeeAhead);
        }
    }
}
