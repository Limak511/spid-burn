using System;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{
    [SerializeField] private float _fireSpreadAngle = 45f;
    [SerializeField] private Transform _firePoint;
    private Vector3 _firePointLocalPosition;
    [SerializeField] private int _fireRaysCount = 5;
    [SerializeField] private float _fireRange = 2f;

    public float FireSpreadAngle => _fireSpreadAngle;




    private void Awake()
    {
        _firePointLocalPosition = _firePoint.localPosition;
    }

    private void Update()
    {
        var mousePosition = GameInput.Instance.GetPlayerWorldMousePosition();
        UpdateBurnerLookAt(mousePosition);
        UpdateFirePointLocalPosition();

        if (GameInput.Instance.IsPlayerAttackPressed())
        {
            Burn();
        }
    }

    private void Burn()
    {
        // Shoot raycasts and collect list without duplicated hits
        var hitObjects = new List<GameObject>();
        var hits = new List<RaycastHit2D>();
        foreach (var fireDirection in GetFireDirections())
        {
            // Current direction hits
            RaycastHit2D[] currentHits = Physics2D.RaycastAll(_firePoint.position, fireDirection, _fireRange);

            // Filter currentHits to avoid duplicates by raycasting in multiple directions
            foreach (var currentHit in currentHits)
            {
                if (!hitObjects.Contains(currentHit.transform.gameObject))
                {
                    hitObjects.Add(currentHit.transform.gameObject);
                    hits.Add(currentHit);
                }
            }
        }

        // Process hits
        foreach (var hit in hits)
        {
            // Burn when object is spider (burnable)
            if (hit.transform.TryGetComponent(out Spider spider))
            {
                Debug.Log($"Spider hit | {spider.gameObject}", spider.gameObject);
            }
        }
    }

    private Vector2[] GetFireDirections()
    {
        // Prepare angle calculations for fire directions
        var firePointAngle = Mathf.Atan2(transform.right.y, transform.right.x);
        var fireAngleUpperLimit = firePointAngle + _fireSpreadAngle / 2 * Mathf.Deg2Rad;
        var fireAngleLowerLimit = firePointAngle - _fireSpreadAngle / 2 * Mathf.Deg2Rad;
        var fireAngleSpreadDifference = (fireAngleUpperLimit - fireAngleLowerLimit) / _fireRaysCount;

        // Store fire directions in the array
        var currentDirectionAngle = fireAngleUpperLimit;
        var fireDirections = new Vector2[_fireRaysCount];
        for (int i = 0; i < _fireRaysCount; i++)
        {
            fireDirections[i] = new Vector2(Mathf.Cos(currentDirectionAngle), Mathf.Sin(currentDirectionAngle));
            currentDirectionAngle -= fireAngleSpreadDifference;
        }

        return fireDirections;
    }

    private void UpdateBurnerLookAt(Vector2 lookAtPosition)
    {
        var rotation = lookAtPosition - (Vector2)transform.position;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg);
    }

    private void UpdateFirePointLocalPosition()
    {
        _firePoint.localPosition = new Vector3(_firePoint.localPosition.x, IsPointingLeft() ? -_firePointLocalPosition.y : _firePointLocalPosition.y, _firePoint.localPosition.z);
    }

    public bool IsPointingLeft()
    {
        return Mathf.Abs(transform.eulerAngles.z) > 90f && Mathf.Abs(transform.eulerAngles.z) < 270f;
    }



    [Header("Gizmos")]
    [Tooltip("Show fire rays always, even when attack is not pressed")]
    [SerializeField] private bool _showRays = false;
    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (!_showRays)
            {
                if (!GameInput.Instance.IsPlayerAttackPressed())
                {
                    return;
                }
            }

            Gizmos.color = Color.green;
            foreach (var fireDirection in GetFireDirections())
            {
                Gizmos.DrawRay(_firePoint.position, fireDirection * _fireRange);
            }
        }
        else
        {
            if (!_showRays)
            {
                return;
            }

            Gizmos.color = Color.green;
            foreach (var fireDirection in GetFireDirections())
            {
                Gizmos.DrawRay(_firePoint.position, fireDirection * _fireRange);
            }
        }
    }
}
