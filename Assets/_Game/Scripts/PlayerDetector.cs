using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerDetector : MonoBehaviour
{
    private Collider2D _collider;

    public event Action<PlayerController> OnPlayerDetected;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController playerController))
        {
            OnPlayerDetected?.Invoke(playerController);
        }
    }
}
